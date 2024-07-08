using Microsoft.AspNetCore.Mvc;
using NuovaAPI.Worker_Services;

namespace NuovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxonomyController : ControllerBase
    {
        private readonly ITaxonomyWorkerService _taxonomyWorkerService;


        public TaxonomyController(ITaxonomyWorkerService taxonomyWorkerService)
        {
            _taxonomyWorkerService = taxonomyWorkerService;
        }

        [HttpPost("Upload")]
        public async Task<IResult> UploadTaxonomy(IFormFile file)
        {
            //if (string.IsNullOrWhiteSpace(file.FileName))
            //{
            //    return Results.BadRequest("File inserito non valido");
            //}

            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("File inserito non valido");
            }

            try
            {
                await _taxonomyWorkerService.AddOrUpdateTaxonomyAsync(file);

                return Results.Ok("Inserimento avvenuto con successo");
            }

            catch (Exception ex)

            {
                return Results.Problem($"Errore: {ex.Message}");
            }
        }
    }
}

