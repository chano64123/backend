using BackendPrueba.Models;
using BackendPrueba.Models.DTO;
using BackendPrueba.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendPrueba.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUserRepository _userRepository;
        protected ResponseDTO _response;

        public UsersController(IUserRepository userRepository) {
            _userRepository = userRepository;
            _response = new ResponseDTO();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> registerUser(UserDTO userDTO) {
            try {
                var user = await _userRepository.registerUser(userDTO, userDTO.password);
                if (user.id <= 0) {
                    _response.displayMessage = "No se pudo registrar el usuario";
                    _response.success = false;
                    return BadRequest(_response);
                }

                _response.displayMessage = "Se registro exitosamente el usuario";
                _response.success = true;
                _response.result = user;
                return Ok(_response);

            } catch (Exception ex) {
                _response.success = false;
                _response.displayMessage = "Error con el servidor";
                _response.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> login(UserDTO userDto) {
            try {
                var user = await _userRepository.login(userDto.userName, userDto.password);
                if (user.id <= 0) {
                    _response.success = false;
                    _response.displayMessage = "No se pudo autentificar el usuario";
                    return NotFound(_response);
                }

                _response.success = true;
                _response.displayMessage = "Bienvenido " + user.userName;
                _response.result = user;
                return Ok(_response);

            } catch (Exception ex) {
                _response.success = false;
                _response.displayMessage = "Error con el servidor";
                _response.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}