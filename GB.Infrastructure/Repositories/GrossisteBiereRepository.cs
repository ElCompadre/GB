using AutoMapper;
using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;
using GB.Domain.Entities;

namespace GB.Infrastructure.Repositories;

public class GrossisteBiereRepository(IGBContext context, IMapper mapper) : IGrossisteBiereRepository
{
    public async Task<GrossisteBiereDTO> AddAsync(GrossisteBiereDTO grossisteBiereDto, CancellationToken cancellationToken = default)
    {
        var addedLink = await context.GrossisteBieres.AddAsync(new GrossisteBiere
        {
            BiereId = grossisteBiereDto.BiereId,
            GrossisteId = grossisteBiereDto.GrossisteId,
        }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return mapper.Map<GrossisteBiereDTO>(addedLink.Entity);
    }

    public void DeleteLinkBiereToGrossiste(int biereId)
    {
        var grossisteBieres = context.GrossisteBieres.Where(gb => gb.BiereId == biereId);
        context.GrossisteBieres.RemoveRange(grossisteBieres);
        context.SaveChanges();
    }

    public bool CheckIfExists(int biereId, int grossisteId)
    {
        return context.GrossisteBieres.Any(gb => gb.BiereId == biereId && gb.GrossisteId == grossisteId);
    }
}