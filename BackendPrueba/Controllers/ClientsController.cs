using BackendPrueba.Models;
using BackendPrueba.Models.DTO;
using BackendPrueba.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendPrueba.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase {
        private readonly IClientRepository _clientRepository;
        protected ResponseDTO _response;

        public ClientsController(IClientRepository clientRepository) {
            _clientRepository = clientRepository;
            _response = new ResponseDTO();
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> getClients() {
            try {
                var clients = await _clientRepository.getClients();
                _response.result = clients;
                _response.displayMessage = "Lista de Clientes";
                _response.success = true;
                return Ok(_response);
            } catch (Exception ex) {
                _response.success = false;
                _response.displayMessage = "Error con el servidor";
                _response.ErrorMessage = new List<string> { ex.ToString()};
                return BadRequest(_response);
            }
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> getClient(int id) {
            try {
                var client = await _clientRepository.getClientById(id);
                if (client == null) {
                    _response.displayMessage = "No se encontro el cliente";
                    _response.success = false;
                    return NotFound(_response);
                }
                _response.success = true;
                _response.result = client;
                _response.displayMessage = "Informacion del Cliente";
                return Ok(_response);
            } catch (Exception ex) {
                _response.success = false;
                _response.displayMessage = "Error con el servidor";
                _response.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> updateClient(int id, ClientDTO client) {
            try {
                ClientDTO clientDTO = await _clientRepository.updateClient(client);
                _response.displayMessage = "Cliente actualizado correctamente";
                _response.result = clientDTO;
                _response.success = true;
                return Ok(_response);
            } catch (Exception ex) {
                _response.success = false;
                _response.displayMessage = "Error con el servidor";
                _response.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientDTO client) {
            try {
                ClientDTO clientDTO = await _clientRepository.addClient(client);
                _response.displayMessage = "Cliente creado correctamente";
                _response.result = clientDTO;
                _response.success = true;
                return CreatedAtAction("GetClient", new { id = clientDTO.id } ,_response);
            } catch (Exception ex) {
                _response.success = false;
                _response.displayMessage = "Error con el servidor";
                _response.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id) {
            try {
                bool clientDeleted = await _clientRepository.deleteClient(id);
                _response.result = clientDeleted;
                if (clientDeleted) {
                    _response.success = true;
                    _response.displayMessage = "Cliente eliminado correctamente";
                    return Ok(_response);
                } else {
                    _response.success = false;
                    _response.displayMessage = "No se pudo eliminar el cliente";
                    return BadRequest(_response);
                }
            } catch (Exception ex) {
                _response.success = false;
                _response.displayMessage = "Error con el servidor";
                _response.ErrorMessage = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
