using GB.Domain.Models;

namespace GB.Application.Interfaces.Repositories;

public interface IBrasserieRepository
{
    public Task<BrasserieDTO> GetByIdAsync(int brasserieId, CancellationToken cancellationToken = default);
    public Task<BrasserieDTO> AddAsync(BrasserieDTO brasserieDto, CancellationToken cancellationToken = default);
    
    public bool CheckIfExists(BrasserieDTO brasserieDto);
}