using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NuovaAPI.Commons.DTO;
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
        public ProdottoController(/*IProdottoManager prodottoManager , */ IProdottoWorkerService prodottoWorkerService, IValidator<Prodotto> prodottoValidator, IValidator<ProdottoDTO> prodottoDTOValidator)
        {
            //_prodottoManager = prodottoManager;
            _prodottoWorkerService = prodottoWorkerService;
            _prodottoValidator = prodottoValidator;
            _prodottoDTOValidator = prodottoDTOValidator;
        }

        [HttpGet]
        public async Task<IResult> GetProdotti()
        {
            var prodotti = await _prodottoWorkerService.GetProduct();
            return Results.Ok(prodotti);
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id)
        {
            return Results.Ok(await _prodottoWorkerService.GetProductId(id));
        }


        //public async Task PostProdotto(Prodotto prodotto)
        //{
        //    await _prodottoManager.AddProdotto(prodotto.NomeProdotto, prodotto.Prezzo, prodotto.IdVetrina);
        //}
        [HttpPost]
        public async Task<IResult> PostProdotto([FromBody] ProdottoDTO prodottoDTO)
        {
            var validationResult = _prodottoDTOValidator.Validate(prodottoDTO);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            if (!ModelState.IsValid)
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

            var validationResult = _prodottoValidator.Validate(prodotto);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            try
            {
                var updatedProduct = await _prodottoWorkerService.PutProduct(id, prodotto);

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
