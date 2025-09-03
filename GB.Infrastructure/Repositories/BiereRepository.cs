using AutoMapper;
using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;
using GB.Domain.Entities;
using GB.Domain.Models;
using Microsoft.Extensions.Logging;

namespace GB.Infrastructure.Repositories;

public class BiereRepository(IGBContext context, IMapper mapper, ILogger<IBiereRepository> logger) : IBiereRepository
{
    public async Task<BiereDTO> AddAsync(BiereDTO biereDto, CancellationToken cancellation = default)
    {
        var addedBiere = await context.Bieres.AddAsync(mapper.Map<BiereDTO, Biere>(biereDto), cancellation);
        await context.SaveChangesAsync(cancellation);
        return mapper.Map<Biere, BiereDTO>(addedBiere.Entity);
    }

    public void Delete(int biereId)
    {
        var biere = context.Bieres.Find(biereId);
        if (biere == null)
        {
            logger.LogWarning("Pas de bière avec l'id: {biereId}", biereId);
            return;
        }
        context.Bieres.Remove(biere);
        context.SaveChanges();
    }

    public bool CheckIfExists(BiereDTO biereDto)
    {
        return context.Bieres.Any(b => (biereDto.Id.HasValue && b.Id == biereDto.Id) || b.Nom == biereDto.Nom);
    }
}