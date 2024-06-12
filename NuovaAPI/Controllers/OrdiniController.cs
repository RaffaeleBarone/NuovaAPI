using Microsoft.AspNetCore.Mvc;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer;
using NuovaAPI.Validators;
using NuovaAPI.Worker_Services;

namespace NuovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdiniController : ControllerBase
    {
        private readonly IOrdiniWorkerService _ordiniWorkerService;
        private readonly AppDbContext _appDbContext;
        public OrdiniController(IOrdiniWorkerService ordiniWorkerService, AppDbContext appDbContext)
        {
            _ordiniWorkerService = ordiniWorkerService;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IResult> GetOrdini(int id)
        {
            var ordine = await _ordiniWorkerService.GetOrdini();
            return Results.Ok(ordine);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id)
        {
            return Results.Ok(await _ordiniWorkerService.GetOrdineId(id));
        }


        [HttpPost]
        public async Task<IResult> PostOrdine([FromBody] OrdiniDTO ordineDTO)
        {
            await _ordiniWorkerService.AddOrdine(ordineDTO);
            _appDbContext.SaveChangesAsync();
            return Results.Ok();
        }

        [HttpPut]
        public async Task<IResult> PutOrdine(int id, OrdiniDTO ordineDTO)
        {
    
            try
            {
                var updatedOrdine = await _ordiniWorkerService.PutOrdine(id, ordineDTO);

                if (updatedOrdine == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(updatedOrdine);
            }
            catch (Exception ex)
            {
                return (IResult)StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpDelete]
        public async Task<IResult> DeleteOrdine(int id)
        {
            var ordine = await _ordiniWorkerService.GetOrdineId(id);
            if (ordine == null)
            {
                return Results.NotFound();
            }

            await _ordiniWorkerService.DeleteOrdine(id);
            return Results.NoContent();
        }
    }
}
