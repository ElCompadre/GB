using GB.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GB.Application.Context;

public class GBContext : DbContext
{
    public GBContext(DbContextOptions<GBContext> options)  : base(options)
    {
        
    }

    public DbSet<Brasserie> Brasseries { get; set; }
    public DbSet<Grossiste> Grossistes { get; set; }
    public DbSet<Biere> Bieres { get; set; }
    public DbSet<GrossisteBrasserie> GrossisteBrasseries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entité avec une clé primaire composite
        modelBuilder.Entity<GrossisteBrasserie>()
            .HasKey(gb => new { gb.GrossisteId, gb.BrasserieId });
        
        modelBuilder.Entity<GrossisteBrasserie>()
            .HasOne(gb => gb.Grossiste)
            .WithMany(g => g.GrossisteBrasseries)
            .HasForeignKey(gb => gb.GrossisteId);
        
        modelBuilder.Entity<GrossisteBrasserie>()
            .HasOne(gb => gb.Brasserie)
            .WithMany(b => b.GrossisteBrasseries)
            .HasForeignKey(gb => gb.BrasserieId);
        
        modelBuilder.Entity<Biere>()
            .HasOne(b => b.Brasserie)
            .WithMany(br => br.Bieres)
            .HasForeignKey(b => b.BrasserieId);
        
        modelBuilder.Entity<Biere>()
            .Property(b => b.Prix);
        
        modelBuilder.Entity<Biere>()
            .Property(b => b.DegresAlcool);
        
        base.OnModelCreating(modelBuilder);
    }
}