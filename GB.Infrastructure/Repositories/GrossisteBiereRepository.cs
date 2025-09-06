using AutoMapper;
using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;
using GB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public void RemoveBiereFromCatalog(int grossisteId, int biereId)
    {
        var grossisteBiere = context.GrossisteBieres.FirstOrDefault(gb => gb.BiereId == biereId && gb.GrossisteId == grossisteId);
        if (grossisteBiere == null) throw new ArgumentNullException(nameof(grossisteBiere));
        context.GrossisteBieres.Remove(grossisteBiere);
        context.SaveChanges();
    }

    public void RemoveBiereFromAllCatalogs(int biereId)
    {
        var grossisteBieres = context.GrossisteBieres.Where(gb => gb.BiereId == biereId);
        context.GrossisteBieres.RemoveRange(grossisteBieres);
        context.SaveChanges();
    }

    public bool CheckIfExists(int grossisteId, int biereId)
    {
        return context.GrossisteBieres.Any(gb => gb.BiereId == biereId && gb.GrossisteId == grossisteId);
    }

    public async Task UpdateQuantityBiereAsync(GrossisteBiereDTO grossisteBiereDto, CancellationToken cancellation = default)
    {
        var grossisteBiere = await context.GrossisteBieres.FirstOrDefaultAsync(gb => gb.BiereId == grossisteBiereDto.BiereId && gb.GrossisteId == grossisteBiereDto.GrossisteId, cancellation);
        if (grossisteBiere == null) throw new ArgumentNullException(nameof(grossisteBiere));
        
        grossisteBiere.Stock = grossisteBiereDto.Stock;

        await context.SaveChangesAsync(cancellation);
    }

    public async Task<GrossisteBiereDTO> GetByIdAsync(int grossisteId, int biereId, CancellationToken cancellationToken = default)
    {
        var grossisteBiere = await context.GrossisteBieres.Include(gb => gb.Biere).FirstOrDefaultAsync(gb => gb.BiereId == biereId && gb.GrossisteId == grossisteId, cancellationToken);
        return grossisteBiere == null ? throw new ArgumentNullException(nameof(grossisteBiere)) : mapper.Map<GrossisteBiereDTO>(grossisteBiere);
    }
}