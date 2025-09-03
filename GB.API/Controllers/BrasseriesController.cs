using AutoMapper;
using GB.Application.Interfaces.Services;
using GB.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrasseriesController(IBrasserieService brasserieService, IMapper mapper) : ControllerBase
    {
        [HttpGet("{brasserieId:int}")]
        [Produces(typeof(GetBrasserieByIdResponseModel))]
        public async Task<IActionResult> GetByIdAsync(int brasserieId)
        {
            return Ok(mapper.Map<BrasserieDTO, GetBrasserieByIdResponseModel>(await brasserieService.GetByIdAsync(brasserieId)));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateBrasserieModel brasserieModel)
        {
            var addedBrasserie = await brasserieService.AddAsync(mapper.Map<CreateBrasserieModel, BrasserieDTO>(brasserieModel));
            return Ok(addedBrasserie);
        }
    }
}
