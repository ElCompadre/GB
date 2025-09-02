using GB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GB.Application.Interfaces;

public interface IGBContext
{
    public DbSet<Brasserie> Brasseries { get; set; }
    public DbSet<Grossiste> Grossistes { get; set; }
    public DbSet<Biere> Bieres { get; set; }
    public DbSet<GrossisteBiere> GrossisteBieres { get; set; }
}