using NuovaAPI.DataLayer.Entities;
using NuovaAPI.Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public interface IClienteManager
    {
        Task AddCliente(Cliente cliente);
        Task RemoveCliente(int id);
        Task<ICollection<Cliente>> GetClienti();
        Task<Cliente> GetIdCliente(int id);
        Task<Cliente> ModificaCliente(int id, ClienteDTO clienteDTO);
    }
}
