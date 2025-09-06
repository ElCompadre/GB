using AutoMapper;
using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;
using GB.Domain.Entities;
using GB.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
        return context.Grossistes.Any(g => (grossisteDto.Id.HasValue && g.Id == grossisteDto.Id) || g.Nom == grossisteDto.Nom);
    }

    public bool CheckIfExists(int grossisteId)
    {
        return context.Grossistes.Any(g => g.Id == grossisteId);
    }

    public async Task<GrossisteDTO> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        return mapper.Map<GrossisteDTO>(await context.Grossistes.FirstOrDefaultAsync(g => g.Id == id, cancellation));
    }
}