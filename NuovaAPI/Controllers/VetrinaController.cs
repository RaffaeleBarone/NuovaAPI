﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.Worker_Services;

namespace NuovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetrinaController : ControllerBase
    {
        //private IVetrinaManager _vetrinaManager;
        private readonly IVetrinaWorkerService _vetrinaWorkerService;
        private readonly IValidator<Vetrina> _vetrinaValidator;
        private readonly IValidator<VetrinaDTO> _vetrinaDTOValidator;
        public VetrinaController(/*IVetrinaManager vetrinaManager, */ IVetrinaWorkerService vetrinaWorkerService, IValidator<Vetrina> vetrinaValidator, IValidator<VetrinaDTO> vetrinaDTOValidator)
        {
            //_vetrinaManager = vetrinaManager;
            _vetrinaWorkerService = vetrinaWorkerService;
            _vetrinaValidator = vetrinaValidator;
            _vetrinaDTOValidator = vetrinaDTOValidator;
        }

        [HttpGet]
        public async Task<IResult> GetVetrine(int id)
        {
            var vetrine = await _vetrinaWorkerService.GetVetrina();
            return Results.Ok(vetrine);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id)
        {
            return Results.Ok(await _vetrinaWorkerService.GetVetrinaId(id));
        }

        [HttpPost]
        public async Task<IResult> PostVetrina([FromBody] VetrinaDTO vetrinaDTO)
        {
            var validationResult = _vetrinaDTOValidator.Validate(vetrinaDTO);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            await _vetrinaWorkerService.AddVetrina(vetrinaDTO);
            return Results.Ok();
        }

        [HttpPut]
        public async Task<IResult> PutVetrina(int id, Vetrina vetrina)
        {
            var validationResult = _vetrinaValidator.Validate(vetrina);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            try
            {
                var updatedVetrina = await _vetrinaWorkerService.PutVetrina(id, vetrina);

                if (updatedVetrina == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(updatedVetrina);
            }
            catch (Exception ex)
            {
                return (IResult)StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        public async Task<IResult> DeleteVetrina(int id)
        {
            var prodotto = await _vetrinaWorkerService.GetVetrinaId(id);
            if (prodotto == null)
            {
                return Results.NotFound();
            }

            await _vetrinaWorkerService.DeleteVetrina(id);
            return Results.NoContent();
        }
    }
}
