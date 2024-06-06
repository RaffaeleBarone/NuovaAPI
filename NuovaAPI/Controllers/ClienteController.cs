using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.Worker_Services;

namespace NuovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //private readonly IClienteManager _clienteManager;
        private readonly IClienteWorkerService _clienteWorkerService;
        private readonly IValidator<Cliente> _clienteValidator;
        private readonly IValidator<ClienteDTO> _clienteDTOValidator;
        public ClienteController(IClienteWorkerService clienteWorkerService, IValidator<Cliente> clienteValidator, IValidator<ClienteDTO> clienteDTOValidator)
        {
            _clienteWorkerService = clienteWorkerService;
            _clienteValidator = clienteValidator;
            _clienteDTOValidator = clienteDTOValidator;
        }


        [HttpGet]
        public async Task<IResult> GetClienti()
        {
            var clienti = await _clienteWorkerService.GetCliente();
            return Results.Ok(clienti);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id)
        {
            return Results.Ok(await _clienteWorkerService.GetClienteId(id));
        }

        [HttpPost]
        public async Task<IResult> PostCliente([FromBody] ClienteDTO clienteDTO)
        {
            var validationResult = _clienteDTOValidator.Validate(clienteDTO);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            if (!ModelState.IsValid)
            {
                return Results.BadRequest(ModelState);
            }

            await _clienteWorkerService.AddCliente(clienteDTO);
            return Results.Ok();
        }

        [HttpPut]
        public async Task<IResult> PutCliente(int id, Cliente cliente)
        {
            var validationResult = _clienteValidator.Validate(cliente);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            try
            {
                var updatedCliente = await _clienteWorkerService.PutCliente(id, cliente);

                if (updatedCliente == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(updatedCliente);
            }
            catch (Exception ex)
            {
                return (IResult)StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        public async Task<IResult> DeleteCliente(int id)
        {
            var clienteDaRimuovere = await _clienteWorkerService.GetClienteId(id);
            if (clienteDaRimuovere == null)
            {
                return Results.NotFound();
            }

            await _clienteWorkerService.DeleteCliente(id);
            return Results.NoContent();
        }
    }
}

