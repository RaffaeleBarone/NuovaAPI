using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure;

namespace NuovaAPI.DataLayer.Manager
{
    public class VetrinaManager : IVetrinaManager
    {

        private readonly IUnitOfWork _unitOfWork;
        public VetrinaManager(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public async Task AddVetrina(Vetrina vetrina)
        {
            _unitOfWork.VetrinaRepository.Add(vetrina);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<VetrinaDTO>> GetVetrine(int? codiceVetrina = null)
        {
            //return _unitOfWork.VetrinaRepository.Get(null)
            //    .Include(v => v.ProdottiInVetrina)
            //    .ToList();

            var query = _unitOfWork.VetrinaRepository.Get(null)
              .Include(x => x.ProdottiInVetrina)
              .AsQueryable();


            //Filtri
            if (codiceVetrina.HasValue)
            {
                query = query.Where(c => c.CodiceVetrina == codiceVetrina.Value);
            }


            var vetrine = await query.ToListAsync();

            var vetrinaDTO = vetrine.Select(x => new VetrinaDTO
            {
                CodiceVetrina = x.CodiceVetrina
            });

            return vetrinaDTO;
        }

        public async Task<Vetrina> GetIdVetrina(int id)
        {
            //return await _unitOfWork.VetrinaRepository.GetById(id)
            //    //.Where(x => x.Id == id)
            //    .Include(v => v.ProdottiInVetrina)
            //    .FirstOrDefault();

            var vetrina = await _unitOfWork.VetrinaRepository
                .Get(v => v.Id == id)
                .Include(v => v.ProdottiInVetrina)
                .SingleOrDefaultAsync();
            return vetrina;
        }

        public async Task RemoveVetrina(int id)
        {
            //using (var dbContextTransaction = _appDbContext.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        var vetrinaDaRimuovere = _appDbContext.Vetrine.Include(v => v.Id).SingleOrDefault(v => v.Id == id);

            //        if (vetrinaDaRimuovere != null)
            //        {
            //            _appDbContext.Vetrine.RemoveRange((IEnumerable<Vetrina>)vetrinaDaRimuovere.ProdottiInVetrina);
            //            _appDbContext.Vetrine.Remove(vetrinaDaRimuovere);

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

            _unitOfWork.BeginTransaction();
            {

                var vetrinaDaRimuovere = _unitOfWork.VetrinaRepository.Delete(id);

                if (vetrinaDaRimuovere != null)
                {
                    _unitOfWork.VetrinaRepository.Delete(vetrinaDaRimuovere);

                    using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
                    ILogger logger = factory.CreateLogger("Program");
                    logger.LogInformation("Modifiche avvenute!");

                    _unitOfWork.Save();
                    _unitOfWork.Commit();
                }
            }
        }

        public async Task<Vetrina> ModificaVetrina(int id, VetrinaDTO vetrinaDTO)
        {
            var vetrinaDaModificare = await _unitOfWork.VetrinaRepository.GetById(id);

            if (vetrinaDaModificare == null)
            {
                throw new Exception("Prodotto non trovato");
            }

            if (vetrinaDTO.CodiceVetrina != null)
            {
                vetrinaDaModificare.CodiceVetrina = (int)vetrinaDTO.CodiceVetrina;
            }

            _unitOfWork.Save();
            return vetrinaDaModificare;
        }
    }
}


