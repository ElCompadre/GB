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

        [HttpPost("{grossisteId:int}/Bieres/{biereId:int}")]
        public async Task<IActionResult> AddBiereToCatalogAsync([FromRoute] int grossisteId, [FromRoute] int biereId, CancellationToken cancellation = default)
        {
            await grossisteService.AddBiereToCatalogAsync(grossisteId, biereId, cancellation);
            return Created();
        }

        [HttpDelete("{grossisteId:int}/Bieres/{biereId:int}")]
        public IActionResult RemoveBiereFromCatalog([FromRoute] int grossisteId, [FromRoute] int biereId, CancellationToken cancellation = default)
        {
            grossisteService.RemoveBiereFromCatalog(grossisteId, biereId, cancellation);
            return Created();
        }
        
        [HttpPut("{grossisteId:int}/Bieres/{biereId:int}")]
        public async Task<IActionResult> UpdateQuantityBiereAsync([FromRoute] int grossisteId, [FromRoute] int biereId, [FromBody] UpdateQuantiteBiereModel updateQuantiteBiereModel, CancellationToken cancellation = default)
        {
            await grossisteService.UpdateQuantityBiereAsync(new GrossisteBiereDTO
            {
                GrossisteId = grossisteId,
                BiereId = biereId,
                Stock = updateQuantiteBiereModel.Quantite
            }, cancellation);
            return Ok();
        }

        [HttpPost("{grossisteId:int}")]
        [Produces(typeof(QuoteResponseModel))]
        public async Task<IActionResult> QuoteRequest([FromRoute] int grossisteId, [FromBody] QuoteRequestModel quoteRequestModel, CancellationToken cancellation = default)
        {
            var quoteRequestDto = mapper.Map<QuoteRequestDTO>(quoteRequestModel);
            quoteRequestDto.GrossisteId = grossisteId;
            var quoteResponse = await grossisteService.QuoteRequestAsync(quoteRequestDto, cancellation);
            return Ok(quoteResponse);
        }
    }
}
