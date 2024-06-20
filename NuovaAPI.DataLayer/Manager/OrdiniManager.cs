using Microsoft.Extensions.Logging;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure;
using System.Data.Entity;

namespace NuovaAPI.DataLayer.Manager
{
    public class OrdiniManager : IOrdiniManager
    {
   
        private readonly IUnitOfWork _unitOfWork;
        public OrdiniManager(IUnitOfWork unitOfWork)
        {
         
            _unitOfWork = unitOfWork;
        }

        public async Task AddAcquisto(Ordini acquisto)
        {
            _unitOfWork.OrdiniRepository.Add(acquisto);
            _unitOfWork.Save();
        }

        public async Task<IQueryable<Ordini>> GetAcquisti()
        {
            //return _appDbContext.Ordini.Include(o => o.ProdottiAcquistati).ToList();
            return _unitOfWork.OrdiniRepository.Get(null);
        }

        public async Task<Ordini> GetIdAcquisto(int id)
        {
            //return _appDbContext.Ordini.Where(x => x.Id == id).Include(o => o.ProdottiAcquistati).SingleOrDefault();
            return await _unitOfWork.OrdiniRepository.Get(o => o.Id == id)
                .Include(o => o.ProdottiAcquistati)
                .SingleOrDefaultAsync();
        }

        public async Task<Ordini> ModificaOrdine(int id, OrdiniDTO ordiniDTO)
        {
            var ordineDaModificare = await _unitOfWork.OrdiniRepository.GetById(id);

            if (ordineDaModificare == null)
            {
                throw new Exception("Ordine non trovato");
            }

            if (ordiniDTO.CodiceOrdine != null)
            {
                ordineDaModificare.CodiceOrdine = ordiniDTO.CodiceOrdine;
            }

            _unitOfWork.Save();
            return ordineDaModificare;
        }

        public async Task RemoveVetrina(int id)
        {
            _unitOfWork.BeginTransaction();
            {

                var ordineDaRimuovere = _unitOfWork.OrdiniRepository.Delete(id);

                if (ordineDaRimuovere != null)
                {
                    _unitOfWork.OrdiniRepository.Delete(ordineDaRimuovere);

                    using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
                    ILogger logger = factory.CreateLogger("Program");
                    logger.LogInformation("Modifiche avvenute!");

                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                }
            }


        }
    }


}

