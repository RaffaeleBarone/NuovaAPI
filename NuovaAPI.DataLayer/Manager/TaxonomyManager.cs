using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure;
using Z.BulkOperations;

namespace NuovaAPI.DataLayer.Manager
{
    public class TaxonomyManager : ITaxonomyManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaxonomyManager(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public async Task AddOrUpdateTaxonomy(List<TaxonomyDTO> taxonomies)
        {
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var now = DateTime.UtcNow;
                var listaTaxonomies = new List<Taxonomy>();
                var listaTermini = new List<Termini>();

                foreach (var taxonomyDto in taxonomies)
                {
                    foreach (var traduzioni in taxonomyDto.Terms)
                    {
                        listaTermini.AddRange(traduzioni.Labels.Select(x => new Termini
                        {
                            Lingua = x.Key,
                            Traduzione = x.Value,
                            TaxonomyId = traduzioni.Id,
                            IsDefault = x.Key == "en_US"
                        }));



                        listaTaxonomies.Add(new Taxonomy
                        {
                            Id = traduzioni.Id,
                            Nome = taxonomyDto.Name,
                            en_US = traduzioni.Labels.FirstOrDefault(x => x.Key == "en_US").Value,
                            isActive = true,
                            LastUpdate = now
                        }); ;


                    }


                }

                Action<BulkOperation<Taxonomy>> bulkTaxonomy = opt => { opt.BatchSize = 500; opt.BatchTimeout = 180; opt.IgnoreOnMergeUpdateExpression = x => x.LastUpdate; };
                await _unitOfWork.TaxonomyRepository.BulkMergeAsync(listaTaxonomies, bulkTaxonomy);



                var existingTaxonomies = _unitOfWork.TaxonomyRepository.Get(x => x.isActive == true && x.LastUpdate < now).ToList(); //isActive true e lastUpdate minore della data din inserimento


                //combinare existingTaxonomies e listaTaxonomies = lista termini da disabilitare e bulk merge su lista da disabilitare
                foreach (var existingTaxonomy in existingTaxonomies)
                {
                    var matchingTaxonomy = listaTaxonomies.FirstOrDefault(x => x.Id == existingTaxonomy.Id);
                    if (matchingTaxonomy == null)
                    {
                        existingTaxonomy.isActive = false;
                    }
                }

                await _unitOfWork.TaxonomyRepository.BulkMergeAsync(existingTaxonomies, bulkTaxonomy);

                Action<BulkOperation<Termini>> bulkTerms = opt => { opt.BatchSize = 500; opt.BatchTimeout = 180; };
                await _unitOfWork.TerminiRepository.BulkMergeAsync(listaTermini, bulkTerms);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                var innerException = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                throw new ApplicationException($"Errore durante l'aggiornamento dei dati: {ex.Message}. Dettagli: {innerException}");
            }
        }



    }
}
