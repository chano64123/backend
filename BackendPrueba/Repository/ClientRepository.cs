using AutoMapper;
using BackendPrueba.Data;
using BackendPrueba.Models;
using BackendPrueba.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BackendPrueba.Repository {
    public class ClientRepository : IClientRepository {

        private readonly ApplicationDBContext _context;
        private IMapper _mapper;

        public ClientRepository(ApplicationDBContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientDTO> addClient(ClientDTO clientDTO) {
            Client client = _mapper.Map<ClientDTO, Client>(clientDTO);
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();
            return _mapper.Map<Client, ClientDTO>(client);
        }

        public async Task<bool> deleteClient(int id) {
            try {
                Client client = await _context.clients.FindAsync(id);
                if (client == null) {
                    return false;
                }
                _context.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ClientDTO> getClientById(int id) {
            Client client = await _context.clients.FindAsync(id);
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task<List<ClientDTO>> getClients() {
            List<Client> clients = await _context.clients.ToListAsync();
            return _mapper.Map<List<ClientDTO>>(clients);
        }

        public async Task<ClientDTO> updateClient(ClientDTO clientDTO) {
            Client client = _mapper.Map<ClientDTO, Client>(clientDTO);
            _context.Update(client);
            await _context.SaveChangesAsync();
            return _mapper.Map<Client, ClientDTO>(client);
        }
    }
}
