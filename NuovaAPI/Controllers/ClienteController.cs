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
        public async Task<IResult> GetClienti([FromQuery] string nome = null, [FromQuery] string cognome = null, [FromQuery] string orderBy = null, [FromQuery] bool ascending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var clienti = await _clienteWorkerService.GetCliente(nome, cognome, orderBy, ascending, pageNumber, pageSize);
                return Results.Ok(clienti);
            }

            catch (Exception ex)
            {
                return Results.Problem($"Errore durante il recupero dei clienti: {ex.Message}");
            }
        }



        //[HttpGet("Ordinati")]
        //public async Task<IResult> GetClientiOrdinati()
        //{
        //    try
        //    {
        //        var clienti = await _clienteWorkerService.GetCliente();

        //        return Results.Ok(clienti);
        //    }

        //    catch (Exception ex)
        //    {
        //        return Results.Problem($"Errore durante il recupero dei clienti: {ex.Message}");
        //    }
        //}


        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id)
        {
            //return Results.Ok(await _clienteWorkerService.GetClienteId(id));
            try
            {
                var cliente = await _clienteWorkerService.GetClienteId(id);

                if (cliente == null)
                {
                    return Results.NotFound($"Cliente con ID {id} non trovato.");
                }
                return Results.Ok(cliente);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Errore durante il recupero del cliente: {ex.Message}");
            }
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
        public async Task<IResult> PutCliente(int id, ClienteDTO clienteDTO)
        {
            var validationResult = _clienteDTOValidator.Validate(clienteDTO);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            try
            {
                var updatedCliente = await _clienteWorkerService.PutCliente(id, clienteDTO);

                if (updatedCliente == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(updatedCliente);
            }
            catch (Exception ex)
            {
                return Results.Problem($"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        public async Task<IResult> DeleteCliente(int id)
        {
            var clienteDaRimuovere = _clienteWorkerService.GetClienteId(id);
            if (clienteDaRimuovere == null)
            {
                return Results.NotFound();
            }

            _clienteWorkerService.DeleteCliente(id);
            return Results.NoContent();
        }

        [HttpPost("Upload")]
        public async Task<IResult> UploadClienti(IFormFile file)
        {
            //if (file == null || file.Length == 0)
            //{
            //    return Results.BadRequest("File inserito non valido");
            //}

            if(string.IsNullOrWhiteSpace(file.FileName))
            {
                return Results.BadRequest("File inserito non valido");
            }

            try
            {
                //var clienti = new List<ClienteDTO>();

                //if (file.FileName.EndsWith(".json"))
                //{
                //    using (var stream = new StreamReader(file.OpenReadStream()))
                //    {
                //        var fileContent = await stream.ReadToEndAsync();
                //        clienti = JsonSerializer.Deserialize<List<ClienteDTO>>(fileContent);
                //    }

                //}

                //else
                //{
                //    return Results.BadRequest("Formato non supportato, per il momento il sistema accetta solo JSON");
                //}

                //    using var stream = new StreamReader(file.OpenReadStream());
                //    var fileContent = await stream.ReadToEndAsync();

                //    var clienti = JsonSerializer.Deserialize<List<ClienteDtoJson>>(fileContent);


                //    if(clienti == null || clienti.Count == 0)
                //    {
                //        return Results.BadRequest("Il file JSON non contiene dati validi");
                //    }

                //    await _clienteWorkerService.AddOrUpdateClientiAsync(clienti);
                //    return Results.Ok("Dati caricati con successo");
                //}

                //catch (Exception ex)
                //{
                //    return Results.Problem($"Errore {ex.Message}");

                await _clienteWorkerService.AddOrUpdateClientiAsync(file);

                return Results.Ok("Inserimento avvenuto con successo");
            }

            catch (Exception ex)

            {
                return Results.Problem($"Errore: {ex.Message}");
            }
        }
    }
}

