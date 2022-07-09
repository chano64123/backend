using BackendPrueba.Models;
using BackendPrueba.Models.DTO;

namespace BackendPrueba.Repository {
    public interface IUserRepository {
        Task<UserDTO> registerUser(UserDTO user, string pass);
        Task<UserDTO> login(string userName, string pass);
        Task<bool> userExist(string userName);
    }
}
