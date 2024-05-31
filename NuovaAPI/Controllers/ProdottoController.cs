using Microsoft.AspNetCore.Mvc;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Manager;
using NuovaAPI.Worker_Services;

namespace NuovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottoController : ControllerBase
    {
        //private IProdottoManager _prodottoManager;
        //public ProdottoController(IProdottoManager prodottoManager)
        //{
        //    _prodottoManager = prodottoManager;
        //}

        private readonly IProdottoWorkerService _prodottoWorkerService;
        public ProdottoController(IProdottoWorkerService prodottoWorkerService)
        {
            _prodottoWorkerService = prodottoWorkerService;
        }

        [HttpGet]
        public async Task<IResult> GetProdotti()
        {
            var prodotti = await _prodottoManager.GetProdotti();
            return Results.Ok(prodotti);
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id)
        {
            return Results.Ok(await _prodottoManager.GetIdProdotto(id));
        }


        //public async Task PostProdotto(Prodotto prodotto)
        //{
        //    await _prodottoManager.AddProdotto(prodotto.NomeProdotto, prodotto.Prezzo, prodotto.IdVetrina);
        //}
        [HttpPost]
        public async Task<IResult> PostProdotto([FromBody] ProdottoDTO prodottoDTO)
        {
            if(!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            //var prodottoWorkerService = new ProdottoWorkerService();
            //Prodotto prodotto = prodottoWorkerService.MapToProdotto(prodottoDTO);
            await _prodottoWorkerService.AddProduct(prodottoDTO);
            return Results.Ok();
        }


        [HttpPut]
        public async Task<IResult> PutProdotto(int id, Prodotto prodotto)
        {
            if (id != prodotto.Id)
            {
                return Results.BadRequest();
            }

            bool updateResult = await _prodottoManager.ModificaProdotto(prodotto);

            if (!updateResult)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }

        [HttpDelete]
        public async Task<IResult> DeleteProdotto(int id)
        {
            var prodotto = await _prodottoManager.GetIdProdotto(id);
            if (prodotto == null)
            {
                return Results.NotFound();
            }

            await _prodottoManager.RemoveProdotto(prodotto.Id);
            return Results.NoContent();
        }

       
    }
}
