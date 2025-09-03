using AutoMapper;
using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;
using GB.Domain.Entities;
using GB.Domain.Models;

namespace GB.Infrastructure.Repositories;

public class GrossisteRepository(IGBContext context, IMapper mapper) : IGrossisteRepository
{
    public async Task<GrossisteDTO> AddAsync(GrossisteDTO grossisteDto, CancellationToken cancellation = default)
    {
        var addedEntity = await context.Grossistes.AddAsync(mapper.Map<Grossiste>(grossisteDto), cancellation);
        await context.SaveChangesAsync(cancellation);
        return mapper.Map<GrossisteDTO>(addedEntity.Entity);
    }

    public bool CheckIfExists(GrossisteDTO grossisteDto)
    {
        return context.Grossistes.Any(g => g.Id == grossisteDto.Id || g.Nom.Equals(grossisteDto.Nom, StringComparison.InvariantCultureIgnoreCase));
    }
}