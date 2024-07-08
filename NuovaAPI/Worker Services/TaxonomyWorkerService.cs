using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Manager;
using System.Text.Json;

namespace NuovaAPI.Worker_Services
{
    public class TaxonomyWorkerService : ITaxonomyWorkerService
    {
        private readonly ITaxonomyManager _taxonomyManager;
        public TaxonomyWorkerService(ITaxonomyManager taxonomyManager)
        {
            _taxonomyManager = taxonomyManager;
        }
        public async Task AddOrUpdateTaxonomyAsync(IFormFile file)
        {
            try
            {
                using var stream = new StreamReader(file.OpenReadStream());
                var fileContent = await stream.ReadToEndAsync();
                var taxonomies = JsonSerializer.Deserialize<List<TaxonomyDTO>>(fileContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (taxonomies == null || taxonomies.Count == 0)
                {
                    throw new ArgumentException("Il file JSON non contiene dati");
                }

                await _taxonomyManager.AddOrUpdateTaxonomy(taxonomies);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Errore: {ex.Message}");
            }
        }


    }
}
