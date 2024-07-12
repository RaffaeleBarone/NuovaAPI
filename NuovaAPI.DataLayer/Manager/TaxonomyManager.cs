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

                Action<BulkOperation<Taxonomy>> bulkTaxonomy = opt => { opt.BatchSize = 100; opt.BatchTimeout = 600; opt.IgnoreOnMergeUpdateExpression = x => x.LastUpdate; };
                await _unitOfWork.TaxonomyRepository.BulkMergeAsync(listaTaxonomies, bulkTaxonomy);

                var existingTaxonomies = _unitOfWork.TaxonomyRepository.Get(x => x.isActive == true && x.LastUpdate < now).ToList(); //isActive true e lastUpdate minore della data di inserimento

                //combinare existingTaxonomies e listaTaxonomies = lista termini da disabilitare e bulk merge su lista da disabilitare    trovare quelle non presenti in listaTaxonomies
                //foreach (var existingTaxonomy in existingTaxonomies)
                //{
                //    var matchingTaxonomy = listaTaxonomies.FirstOrDefault(x => x.Id == existingTaxonomy.Id);
                //    if (matchingTaxonomy == null)
                //    {
                //        existingTaxonomy.isActive = false;
                //    }
                //}

                //await _unitOfWork.TaxonomyRepository.BulkMergeAsync(existingTaxonomies, bulkTaxonomy);

                var toDisable = existingTaxonomies
                    .Where(existing => !listaTaxonomies.Any(newTaxonomy => newTaxonomy.Id == existing.Id))
                    .Select(taxonomy => { taxonomy.isActive = false; return taxonomy; })
                    .ToList();

                if (toDisable.Any())
                {
                    await _unitOfWork.TaxonomyRepository.BulkMergeAsync(toDisable, bulkTaxonomy);
                }

                Action<BulkOperation<Termini>> bulkTerms = opt => { opt.BatchSize = 100; opt.BatchTimeout = 600; };
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
