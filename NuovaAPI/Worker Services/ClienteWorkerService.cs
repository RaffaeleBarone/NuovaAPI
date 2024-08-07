﻿using AutoMapper;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;
using System.Text.Json;

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

        public async Task<IEnumerable<ClienteDTO>> GetCliente(string nome = null, string cognome = null, string orderBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            return await _clienteManager.GetClienti(nome, cognome, orderBy, ascending, pageNumber, pageSize);
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

        //public async Task AddOrUpdateClientiAsync(List<ClienteDtoJson> clienti)
        //{
        //    await _clienteManager.AddOrUpdateClienti(clienti);
        //}


        public async Task AddOrUpdateClientiAsync(IFormFile file)
        {
            try
            {
                using var stream = new StreamReader(file.OpenReadStream());
                var fileContent = await stream.ReadToEndAsync();
                var clienti = JsonSerializer.Deserialize<List<ClienteDtoJson>>(fileContent);

                if (clienti == null || clienti.Count == 0)
                {
                    throw new ArgumentException("Il file JSON non contiene dati");
                }

                await _clienteManager.AddOrUpdateClienti(clienti);
            }

            catch (Exception ex)
            {
                throw new ApplicationException($"Errore: {ex.Message}");
            }
        }
    }
}
