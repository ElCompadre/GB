using AutoMapper;
using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;
using GB.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class BrasserieRepository(IGBContext context, IMapper mapper) : IBrasserieRepository
{
    private IGBContext GbContext { get; set; } = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<BrasserieDTO> GetByIdAsync(int brasserieId)
    {
        var brasserie = await GbContext.Brasseries
            .Include(b => b.Bieres)
            .Include(b => b.GrossisteBrasseries)
            .FirstOrDefaultAsync(b => b.Id == brasserieId);
        if (brasserie == null) throw new ArgumentNullException(nameof(brasserie));

        return mapper.Map<BrasserieDTO>(brasserie);
    }
}