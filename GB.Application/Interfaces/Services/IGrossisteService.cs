using GB.Domain.Models;

namespace GB.Application.Interfaces.Services;

public interface IGrossisteService
{
    public Task<GrossisteDTO> AddAsync(GrossisteDTO grossisteDto, CancellationToken cancellation = default);
    public Task AddBiereToCatalogAsync(int grossisteId, int biereId, CancellationToken cancellation = default);
    public void RemoveBiereFromCatalog(int grossisteId, int biereId, CancellationToken cancellation = default);
    public Task UpdateQuantityBiereAsync(GrossisteBiereDTO grossisteBiereDto, CancellationToken cancellation = default);
    public Task<QuoteResponseDTO> QuoteRequestAsync(QuoteRequestDTO quoteRequestDto, CancellationToken cancellation = default);
}