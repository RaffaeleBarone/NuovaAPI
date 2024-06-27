using AutoMapper;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;

namespace NuovaAPI.Worker_Services
{
    public class ClienteWorkerService : IClienteWorkerService
    {
        private readonly IClienteManager _clienteManager;
        private readonly IMapper _mapper;

        public ClienteWorkerService(IClienteManager clienteManager, IMapper mapper)
        {
            _clienteManager = clienteManager;
            _mapper = mapper;
        }

        //public Cliente MapToCliente(ClienteDTO clienteDTO)      COMMENTATO PERCHÈ FATTO CON AUTOMAPPER
        //{
        //    var cliente = new Cliente();
        //    cliente.Id = clienteDTO.Id;
        //    return cliente;
        //}

        public Cliente MapToCliente(ClienteDTO clienteDTO)
        {
            return _mapper.Map<Cliente>(clienteDTO);
        }

        public async Task AddCliente(ClienteDTO clienteDTO)
        {
            var cliente = MapToCliente(clienteDTO);
            await _clienteManager.AddCliente(cliente);
        }

        public async Task<IEnumerable<ClienteDTO>> GetCliente(string nome = null, string cognome = null)
        {
            return await _clienteManager.GetClienti(nome, cognome);
        }


        public async Task<ClienteDTOByID> GetClienteId(int id)
        {
            return await _clienteManager.GetIdCliente(id);
        }

        public async Task<Cliente> PutCliente(int id, ClienteDTO clienteDTO)
        {
            return await _clienteManager.ModificaCliente(id, clienteDTO);
        }

        public async Task DeleteCliente(int id)
        {
            await _clienteManager.RemoveCliente(id);
        }
    }
}
