using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IClienteWorkerService
    {
        Cliente MapToCliente(ClienteDTO clienteDTO);
        Task AddCliente(ClienteDTO clienteDTO);
        Task<IEnumerable<ClienteDTO>> GetCliente();
        Task<ClienteDTOByID> GetClienteId(int id);
        Task<Cliente> PutCliente(int id, ClienteDTO clienteDTO);
        Task DeleteCliente(int id);
    }
}
