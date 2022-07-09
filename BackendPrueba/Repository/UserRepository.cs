using AutoMapper;
using BackendPrueba.Data;
using BackendPrueba.Models;
using BackendPrueba.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BackendPrueba.Repository {
    public class UserRepository : IUserRepository {

        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configration;
        private IMapper _mapper;

        public UserRepository(ApplicationDBContext context, IMapper mapper, IConfiguration configration) {
            _context = context;
            _mapper = mapper;
            _configration = configration;
        }

        public async Task<UserDTO> login(string userName, string pass) {
            try {
                var user = await _context.users.FirstOrDefaultAsync(x => x.userName.ToLower().Equals(userName.ToLower()));
                
                if (user == null) { 
                    return new UserDTO();
                }

                if (!verificarPassWordHash(pass, user.passwordHash, user.passwordSalt)) {
                    return new UserDTO();
                }

                user.token = crearToken(user);
                return _mapper.Map<User, UserDTO>(user);
            } catch (Exception) {
                return new UserDTO();
            }
        }

        public async Task<UserDTO> registerUser(UserDTO userDTO, string pass) {
            try {
                User user = _mapper.Map<UserDTO, User>(userDTO);

                if (await userExist(user.userName)) {
                    return new UserDTO();
                }

                createPassHash(pass, out byte[] passwordHash, out byte[] passwordSalt);

                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                user = await _context.users.FindAsync(user.id);
                return _mapper.Map<User, UserDTO>(user);
            } catch (Exception) {
                return new UserDTO();
            }
        }

        public async Task<bool> userExist(string userName) {
            try {
                return await _context.users.AnyAsync(x => x.userName.ToLower().Equals(userName));
            } catch (Exception) {
                return false;
            }
        }

        private void createPassHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool verificarPassWordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) {
                        return false;
                    }
                }
                return true;
            }
        }

        private string crearToken(User user) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.userName)
            };

            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandle = new JwtSecurityTokenHandler();

            var token = tokenHandle.CreateToken(tokenDescriptor);

            return tokenHandle.WriteToken(token);
        }
    }
}
