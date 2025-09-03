using GB.Application.Interfaces.Repositories;
using GB.Application.Interfaces.Services;
using GB.Domain.Models;

namespace GB.Application.Services;

public class BrasserieService(IBrasserieRepository brasserieRepository) : IBrasserieService
{
    public Task<BrasserieDTO> GetByIdAsync(int brasserieId)
    {
        return brasserieRepository.GetByIdAsync(brasserieId);
    }

    public ICollection<BrasserieDTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<BrasserieDTO> AddAsync(BrasserieDTO brasserieDto, CancellationToken cancellation = default)
    {
        return await brasserieRepository.AddAsync(brasserieDto, cancellation);
    }
}