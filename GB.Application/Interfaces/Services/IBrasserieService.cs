using GB.Domain.Models;

namespace GB.Application.Interfaces.Services;

public interface IBrasserieService
{
    public Task<BrasserieDTO> GetByIdAsync(int brasserieId);
    
    public Task<BrasserieDTO> AddAsync(BrasserieDTO brasserieDto, CancellationToken cancellation = default);
}