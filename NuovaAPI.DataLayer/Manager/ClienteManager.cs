using Microsoft.Extensions.Logging;
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
            //using (var dbContextTransaction = _appDbContext.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        var clienteDaRimuovere = _appDbContext.Clienti.Include(c => c.Id).SingleOrDefault(c => c.Id == id);

            //        if (clienteDaRimuovere != null)
            //        {
            //            _appDbContext.Clienti.RemoveRange((IEnumerable<Cliente>)clienteDaRimuovere);
            //            _appDbContext.Clienti.Remove(clienteDaRimuovere);

            //            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            //            ILogger logger = factory.CreateLogger("Program");
            //            logger.LogInformation("Modifiche avvenute!");

            //            await _appDbContext.SaveChangesAsync();
            //            dbContextTransaction.Commit();
            //        }
            //    }

            //    catch (Exception)
            //    {
            //        dbContextTransaction.Rollback();
            //    }

        //    }
        }

        public async Task<ICollection<Cliente>> GetClienti()
        {
            return await _appDbContext.Clienti.ToListAsync();
        }

        public async Task<Cliente> GetIdCliente(int id)
        {
            return _appDbContext.Clienti.Where(x => x.Id == id).SingleOrDefault();
        }

        public async Task<Cliente> ModificaCliente(int id, Cliente cliente)
        {
            var clienteDaModificare = await _appDbContext.Clienti.FindAsync(cliente.Id);

            if (clienteDaModificare == null)
            {
                throw new Exception("Cliente non trovato");
            }

            clienteDaModificare.Nome = cliente.Nome;
            clienteDaModificare.Cognome = cliente.Cognome;

            await _appDbContext.SaveChangesAsync();
            return clienteDaModificare;
        }
    }
}
