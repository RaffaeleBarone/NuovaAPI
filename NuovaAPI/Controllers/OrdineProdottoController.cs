using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.Worker_Services;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NuovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdineProdottoController : ControllerBase
    {
        private readonly IOrdineProdottoWorkerService _ordineProdottoWorkerService;
        private readonly AppDbContext _appDbContext;

        public OrdineProdottoController(IOrdineProdottoWorkerService ordineProdottoWorkerService, AppDbContext appDbContext)
        {
            _ordineProdottoWorkerService = ordineProdottoWorkerService;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IResult> GetOrdiniProdotti(int idOrdine)
        {
            var ordiniProdotti = await _ordineProdottoWorkerService.GetOrdiniProdotti(idOrdine);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            string json = JsonSerializer.Serialize(ordiniProdotti, options);

            return Results.Ok(json);
        }

        [HttpGet("{idOrdine}/{idProdotto}")]
        public async Task<IResult> GetById([FromRoute] int idOrdine, [FromRoute] int idProdotto)
        {
            var ordineProdotto = await _ordineProdottoWorkerService.GetOrdineProdottoById(idOrdine, idProdotto);
            if (ordineProdotto == null)
            {
                return Results.NotFound($"OrdineProdotto con ID Ordine {idOrdine} e ID Prodotto {idProdotto} non trovato.");
            }
            return Results.Ok(ordineProdotto);
        }

        [HttpPost]
        public async Task<IResult> PostOrdineProdotto([FromBody] OrdineProdottoDTO ordineProdottoDTO)
        {
            await _ordineProdottoWorkerService.AddOrdineProdotto(ordineProdottoDTO);
            _appDbContext.SaveChangesAsync();
            return Results.Ok();
        }

        [HttpPut("{idOrdine}/{idProdotto}")]
        public async Task<IResult> PutOrdineProdotto(int idOrdine, int idProdotto, [FromBody] OrdineProdottoDTO ordineProdottoDTO)
        {
            var updatedOrdineProdotto = await _ordineProdottoWorkerService.PutOrdineProdotto(idOrdine, idProdotto, ordineProdottoDTO);
            if (updatedOrdineProdotto == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(updatedOrdineProdotto);
        }

        [HttpDelete("{idOrdine}/{idProdotto}")]
        public async Task<IResult> DeleteOrdineProdotto(int idOrdine, int idProdotto)
        {
            var ordineProdotto = await _ordineProdottoWorkerService.GetOrdineProdottoById(idOrdine, idProdotto);
            if (ordineProdotto == null)
            {
                return Results.NotFound();
            }
            await _ordineProdottoWorkerService.DeleteOrdineProdotto(idOrdine, idProdotto);
            return Results.NoContent();
        }
    }
}
