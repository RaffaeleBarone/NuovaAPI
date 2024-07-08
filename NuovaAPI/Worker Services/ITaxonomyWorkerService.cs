namespace NuovaAPI.Worker_Services
{
    public interface ITaxonomyWorkerService
    {
        Task AddOrUpdateTaxonomyAsync(IFormFile file);
    }
}
