using Microsoft.Extensions.Logging;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using System.Data.Entity;

namespace NuovaAPI.DataLayer.Manager
{
    public class ClienteManager : IClienteManager
    {
        private readonly AppDbContext _appDbContext;
        public ClienteManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddCliente(Cliente cliente)
        {
            _appDbContext.Clienti.Add(cliente);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task RemoveCliente(int id)
        {
            using (var dbContextTransaction = _appDbContext.Database.BeginTransaction())
            {
                try
                {
                    var clienteDaRimuovere = _appDbContext.Clienti.SingleOrDefault(c => c.Id == id);

                    if (clienteDaRimuovere != null)
                    {
                        _appDbContext.Clienti.Remove(clienteDaRimuovere);

                        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
                        ILogger logger = factory.CreateLogger("Program");
                        logger.LogInformation("Modifiche avvenute!");

                        await _appDbContext.SaveChangesAsync();
                        dbContextTransaction.Commit();
                    }
                }

                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }

            }
        }

        public async Task<ICollection<Cliente>> GetClienti()
        {
            return _appDbContext.Clienti.ToList();
        }

        public async Task<Cliente> GetIdCliente(int id)
        {
            return _appDbContext.Clienti.Where(x => x.Id == id)
                .Include(c => c.Ordini)
                .SingleOrDefault();
        }

        public async Task<Cliente> ModificaCliente(int id, ClienteDTO clienteDTO)
        {
            var clienteDaModificare = await _appDbContext.Clienti.FindAsync(id);

            if (clienteDaModificare == null)
            {
                throw new Exception("Cliente non trovato");
            }

            if (clienteDTO.Nome != null)
            {
                clienteDaModificare.Nome = clienteDTO.Nome;
            }

            if (clienteDTO.Cognome != null)
            {
                clienteDaModificare.Cognome = clienteDTO.Cognome;
            }

            if (clienteDTO.DataDiNascita != null)
            {
                clienteDaModificare.DataDiNascita = (DateTime)clienteDTO.DataDiNascita;
            }
            await _appDbContext.SaveChangesAsync();
            return clienteDaModificare;
        }
    }
}
