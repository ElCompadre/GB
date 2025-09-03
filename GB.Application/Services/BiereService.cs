using GB.Application.Interfaces.Repositories;
using GB.Application.Interfaces.Services;
using GB.Domain.Models;

namespace GB.Application.Services;

public class BiereService(IBiereRepository biereRepository, IGrossisteBiereRepository grossisteBiereRepository): IBiereService
{
    public Task<BiereDTO> AddAsync(BiereDTO biereDto, CancellationToken cancellationToken = default)
    {
        return biereRepository.AddAsync(biereDto, cancellationToken);
    }

    public void Delete(int biereId)
    {
        grossisteBiereRepository.DeleteLinkBiereToGrossiste(biereId);
        biereRepository.Delete(biereId);
    }
}