using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IClienteWorkerService
    {
        Cliente MapToCliente(ClienteDTO clienteDTO);
        Task AddCliente(ClienteDTO clienteDTO);
        Task<ICollection<Cliente>> GetCliente();
        Task<Cliente> GetClienteId(int id);
        Task<Cliente> PutCliente(int id, Cliente cliente);
        Task DeleteCliente(int id);
    }
}
