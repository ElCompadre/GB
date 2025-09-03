using AutoMapper;
using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;
using GB.Domain.Entities;
using GB.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class BrasserieRepository(IGBContext context, IMapper mapper) : IBrasserieRepository
{
    public async Task<BrasserieDTO> GetByIdAsync(int brasserieId, CancellationToken cancellationToken = default)
    {
        var brasserie = await context.Brasseries
            .Include(b => b.Bieres)
            .Include(b => b.GrossisteBrasseries)
            .FirstOrDefaultAsync(b => b.Id == brasserieId, cancellationToken);

        return mapper.Map<BrasserieDTO>(brasserie);
    }

    public async Task<BrasserieDTO> AddAsync(BrasserieDTO brasserieDto, CancellationToken cancellationToken = default)
    {
        var addedBrasserie = await context.Brasseries.AddAsync(mapper.Map<Brasserie>(brasserieDto), cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return mapper.Map<BrasserieDTO>(addedBrasserie.Entity);
    }
}