using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuovaAPI.DataLayer.Manager;

namespace NuovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetrinaController : ControllerBase
    {
        private IVetrinaManager _vetrinaManager;
        public VetrinaController(IVetrinaManager vetrinaManager)
        {
            _vetrinaManager = vetrinaManager;
        }

        [HttpGet]
        public async Task<IResult> GetVetrine(int id)
        {
            var vetrine = await _vetrinaManager.GetVetrine();
            return Results.Ok(vetrine);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetById([FromRoute] int id)
        {
            return Results.Ok(await _vetrinaManager.GetIdVetrina(id));
        }
    }
}
