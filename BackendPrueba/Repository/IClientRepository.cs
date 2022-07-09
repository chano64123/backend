using BackendPrueba.Models.DTO;

namespace BackendPrueba.Repository {
    public interface IClientRepository {
        Task<List<ClientDTO>> getClients();
        Task<ClientDTO> getClientById(int id);
        Task<ClientDTO> addClient(ClientDTO clientDTO);
        Task<ClientDTO> updateClient(ClientDTO clientDTO);
        Task<bool> deleteClient(int id);
    }
}
