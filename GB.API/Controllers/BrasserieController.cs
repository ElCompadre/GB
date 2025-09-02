using AutoMapper;
using GB.Application.Interfaces.Services;
using GB.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrasserieController(IBrasserieService brasserieService, IMapper mapper) : ControllerBase
    {
        [HttpGet("{brasserieId:int}")]
        [Produces(typeof(GetBrasserieByIdResponseModel))]
        public async Task<IActionResult> GetByIdAsync(int brasserieId)
        {
            return Ok(mapper.Map<BrasserieDTO, GetBrasserieByIdResponseModel>(await brasserieService.GetByIdAsync(brasserieId)));
        }
    }
}
