using GB.Application.Interfaces.Repositories;
using GB.Application.Interfaces.Services;
using GB.Domain.Errors;
using GB.Domain.Models;

namespace GB.Application.Services;

public class BrasserieService(IBrasserieRepository brasserieRepository) : IBrasserieService
{
    public Task<BrasserieDTO> GetByIdAsync(int brasserieId)
    {
        return brasserieRepository.GetByIdAsync(brasserieId);
    }

    public Task<BrasserieDTO> AddAsync(BrasserieDTO brasserieDto, CancellationToken cancellation = default)
    {
        if (brasserieRepository.CheckIfExists(brasserieDto))
        {
            throw new EntityAlreadyExistsException("Une brasserie avec cet id ou ce nom existe déjà");
        }
        return brasserieRepository.AddAsync(brasserieDto, cancellation);
    }
}