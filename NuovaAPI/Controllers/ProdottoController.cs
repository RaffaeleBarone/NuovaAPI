using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.Worker_Services;

namespace NuovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottoController : ControllerBase
    {
        //private IProdottoManager _prodottoManager;
        private readonly IProdottoWorkerService _prodottoWorkerService;
        private readonly IValidator<Prodotto> _prodottoValidator;
        private readonly IValidator<ProdottoDTO> _prodottoDTOValidator;
        private readonly AppDbContext _appDbContext;
        public ProdottoController(IProdottoWorkerService prodottoWorkerService, IValidator<Prodotto> prodottoValidator, IValidator<ProdottoDTO> prodottoDTOValidator, AppDbContext appDbContext)
        {
            //_prodottoManager = prodottoManager;
            _prodottoWorkerService = prodottoWorkerService;
            _prodottoValidator = prodottoValidator;
            _prodottoDTOValidator = prodottoDTOValidator;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IResult> GetProdotti([FromQuery] string nomeProdotto = null)
        {
            try
            {
                var prodotti = await _prodottoWorkerService.GetProduct(nomeProdotto);
                return Results.Ok(prodotti);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Errore durante il recupero dei prodotti: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id)
        {
            //return Results.Ok(await _prodottoWorkerService.GetProductId(id));
            try
            {
                var prodotto = await _prodottoWorkerService.GetProductId(id);
                if (prodotto == null)
                {
                    return Results.NotFound($"Prodotto con ID {id} non trovato.");
                }
                return Results.Ok(prodotto);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Errore durante il recupero del prodotto: {ex.Message}");
            }

        }


        //public async Task PostProdotto(Prodotto prodotto)
        //{
        //    await _prodottoManager.AddProdotto(prodotto.NomeProdotto, prodotto.Prezzo, prodotto.IdVetrina);
        //}
        //[HttpPost]
        //public async Task<IResult> PostProdotto([FromBody] ProdottoDTO prodottoDTO)
        //{
        //    var validationResult = _prodottoDTOValidator.Validate(prodottoDTO);
        //    if (!validationResult.IsValid)
        //    {
        //        return Results.BadRequest(validationResult.Errors);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return Results.BadRequest(ModelState);
        //    }

        //    //var prodottoWorkerService = new ProdottoWorkerService();
        //    //Prodotto prodotto = prodottoWorkerService.MapToProdotto(prodottoDTO);
        //    await _prodottoWorkerService.AddProduct(prodottoDTO);
        //    return Results.Ok();
        //}

        [HttpPost]
        public async Task<IResult> PostProdotto([FromBody] ProdottoDTO prodottoDTO)
        {
            await _prodottoWorkerService.AddProduct(prodottoDTO);


            //if (prodotto.Id == 0)
            //{
            //    throw new Exception("ID is not generated!");
            //}

            return Results.Ok(prodottoDTO);
        }


        [HttpPut]
        public async Task<IResult> PutProdotto(int id, ProdottoDTO prodottoDTO)
        {
            //if (id != prodotto.Id)
            //{
            //    return Results.BadRequest();
            //}

            //bool updateResult = await _prodottoWorkerService.PutProduct(id, prodotto);

            //if (!updateResult)
            //{
            //    return Results.NotFound();
            //}

            //return Results.NoContent();

            var validationResult = _prodottoDTOValidator.Validate(prodottoDTO);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            try
            {
                var updatedProduct = await _prodottoWorkerService.PutProduct(id, prodottoDTO);

                if (updatedProduct == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return (IResult)StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        public async Task<IResult> DeleteProdotto(int id)
        {
            var prodotto = await _prodottoWorkerService.GetProductId(id);
            if (prodotto == null)
            {
                return Results.NotFound();
            }

            await _prodottoWorkerService.DeleteProduct(id);
            return Results.NoContent();
        }
    }
}
