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
        public async Task<IActionResult> AddAsync([FromBody] GrossisteDTO grossisteDto, CancellationToken cancellation = default)
        {
            var addedGrossiste = grossisteService.AddAsync(grossisteDto, cancellation);
            return Ok(addedGrossiste);
        }
    }
}
