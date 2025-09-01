using GB.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GB.Application.Interfaces;

public interface IGBContext
{
    public DbSet<Brasserie> Brasseries { get; set; }
    public DbSet<Grossiste> Grossistes { get; set; }
    public DbSet<Biere> Bieres { get; set; }
    public DbSet<GrossisteBrasserie> GrossisteBrasseries { get; set; }
}