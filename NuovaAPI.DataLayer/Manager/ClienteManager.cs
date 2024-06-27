using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NuovaAPI.DataLayer.Manager
{
    public class ClienteManager : IClienteManager
    {

        private readonly IUnitOfWork _unitOfWork;
        public ClienteManager(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task AddCliente(Cliente cliente)
        {
            //_appDbContext.Clienti.Add(cliente);
            //await _appDbContext.SaveChangesAsync();

            _unitOfWork.ClienteRepository.Add(cliente);
            await _unitOfWork.ClienteRepository.SaveAsync();

        }

        public async Task RemoveCliente(int id)
        {
            _unitOfWork.BeginTransaction();
            {

                //var clienteDaRimuovere = _appDbContext.Clienti.SingleOrDefault(c => c.Id == id);
                var clienteDaRimuovere = _unitOfWork.ClienteRepository.Delete(id);

                if (clienteDaRimuovere != null)
                {
                    //_appDbContext.Clienti.Remove(clienteDaRimuovere);
                    _unitOfWork.ClienteRepository.Delete(clienteDaRimuovere);

                    using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
                    ILogger logger = factory.CreateLogger("Program");
                    logger.LogInformation("Modifiche avvenute!");

                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                }
            }
        }

        public async Task<IEnumerable<ClienteDTO>> GetClienti(string nome = null, string cognome = null, string orderBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            var query = _unitOfWork.ClienteRepository.Get(null)
                .Include(x => x.Ordini)
                .ThenInclude(x => x.ProdottiAcquistati)
                .ThenInclude(x => x.Prodotto)
                .AsQueryable();

            // Filtri
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(c => c.Nome.Contains(nome));
            }

            if (!string.IsNullOrEmpty(cognome))
            {
                query = query.Where(c => c.Cognome.Contains(cognome));
            }


            // Ordinamento
            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy.ToLower())
                {
                    case "nome":
                        query = ascending ? query.OrderBy(c => c.Nome) : query.OrderByDescending(c => c.Nome);
                        break;
                    case "cognome":
                        query = ascending ? query.OrderBy(c => c.Cognome) : query.OrderByDescending(c => c.Cognome);
                        break;
                    case "datadinascita":
                        query = ascending ? query.OrderBy(c => c.DataDiNascita) : query.OrderByDescending(c => c.DataDiNascita);
                        break;
                    default:
                        query = ascending ? query.OrderBy(c => c.Nome) : query.OrderByDescending(c => c.Nome);
                        break;
                }
            }



            //Paginazione

            var skipAmount = (pageNumber - 1) * pageSize;
            query = query.Skip(skipAmount).Take(pageSize);

            var clienti = await query.ToListAsync();

            var clientiDTO = clienti.Select(x => new ClienteDTO
            {
                Cognome = x.Cognome,
                DataDiNascita = x.DataDiNascita,
                Nome = x.Nome,
                Ordini = x.Ordini?.Select(o => new OrdiniDTO
                {
                    CodiceOrdine = o.CodiceOrdine,
                    Costo = o.ProdottiAcquistati?.Sum(p => p.Prodotto?.Prezzo) ?? 0,
                    Prodotti = o.ProdottiAcquistati?.Select(x => x.Prodotto?.NomeProdotto ?? "NA")
                })
            });

            return clientiDTO;
        }

        public async Task<ClienteDTOByID> GetIdCliente(int id)
        {
            //return _unitOfWork.ClienteRepository.Where(x => x.Id == id)
            //    .Include(c => c.Ordini)
            //    .SingleOrDefault();

            var cliente = await _unitOfWork.ClienteRepository.GetById(id);
            //    .Include(x => x.Ordini)
            //    .ThenInclude(x => x.ProdottiAcquistati)
            //    .ThenInclude(x => x.Prodotto)
            //    .FirstOrDefaultAsync();

            if (cliente == null)
            {
                return null;
            }

            var clienteDTO = new ClienteDTOByID
            {
                Cognome = cliente.Cognome,
                DataDiNascita = cliente.DataDiNascita,
                Nome = cliente.Nome,
                //Ordini = cliente.Ordini?.Select(o => new OrdiniDTO
                //{
                //    CodiceOrdine = o.CodiceOrdine,
                //    Costo = o.ProdottiAcquistati?.Sum(p => p.Prodotto?.Prezzo) ?? 0,
                //    Prodotti = o.ProdottiAcquistati?.Select(x => x.Prodotto?.NomeProdotto ?? "NA")
                //})
            };

            return clienteDTO;
        }


        public async Task<Cliente> ModificaCliente(int id, ClienteDTO clienteDTO)
        {
            //var clienteDaModificare = await _appDbContext.Clienti.FindAsync(id);
            var clienteDaModificare = await _unitOfWork.ClienteRepository.GetById(id);

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

            _unitOfWork.Save();
            return clienteDaModificare;
        }
    }
}