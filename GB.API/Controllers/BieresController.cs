using AutoMapper;
using GB.Application.Interfaces.Services;
using GB.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BieresController(IBiereService biereService, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateBiereModel createBiereModel,
            CancellationToken cancellation = default)
        {
            var addedBiere = await biereService.AddAsync(mapper.Map<CreateBiereModel, BiereDTO>(createBiereModel), cancellation);
            return Ok(addedBiere);
        }
        
        [HttpDelete("{biereId:int}")]
        public IActionResult Delete(int biereId)
        {
            biereService.Delete(biereId);
            return NoContent();
        }
    }
}
