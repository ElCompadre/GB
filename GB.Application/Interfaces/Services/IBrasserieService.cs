using GB.Domain.Models;

namespace GB.Application.Interfaces.Services;

public interface IBrasserieService
{
    public Task<BrasserieDTO> GetByIdAsync(int brasserieId);
    public ICollection<BrasserieDTO> GetAll();
    
    public Task<BrasserieDTO> AddAsync(BrasserieDTO brasserieDto, CancellationToken cancellation = default);
}