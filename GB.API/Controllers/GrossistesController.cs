using AutoMapper;
using GB.Application.Interfaces.Services;
using GB.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrossistesController(IGrossisteService grossisteService, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateGrossisteModel createGrossisteModel, CancellationToken cancellation = default)
        {
            var addedGrossiste = await grossisteService.AddAsync(mapper.Map<GrossisteDTO>(createGrossisteModel), cancellation);
            return Ok(addedGrossiste);
        }
    }
}
