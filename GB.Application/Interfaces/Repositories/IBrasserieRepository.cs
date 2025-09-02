using GB.Domain.Models;

namespace GB.Application.Interfaces.Repositories;

public interface IBrasserieRepository
{
    public Task<BrasserieDTO> GetByIdAsync(int brasserieId);
}