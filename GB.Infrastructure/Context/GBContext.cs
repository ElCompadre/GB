using GB.Application.Interfaces;
using GB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GB.Infrastructure.Context;

public class GBContext : DbContext, IGBContext
{
    public GBContext(DbContextOptions<GBContext> options)  : base(options)
    {
    }

    public DbSet<Brasserie> Brasseries { get; set; }
    public DbSet<Grossiste> Grossistes { get; set; }
    public DbSet<Biere> Bieres { get; set; }
    public DbSet<GrossisteBiere> GrossisteBieres { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GrossisteBiere>()
            .HasKey(gb => new { gb.GrossisteId, gb.BiereId });
        
        modelBuilder.Entity<GrossisteBiere>()
            .HasOne(gb => gb.Grossiste)
            .WithMany(g => g.GrossisteBieres)
            .HasForeignKey(gb => gb.GrossisteId);
        
        modelBuilder.Entity<GrossisteBiere>()
            .HasOne(gb => gb.Biere)
            .WithMany(b => b.GrossisteBieres)
            .HasForeignKey(gb => gb.BiereId);
        
        modelBuilder.Entity<Biere>()
            .HasOne(b => b.Brasserie)
            .WithMany(br => br.Bieres)
            .HasForeignKey(b => b.BrasserieId);
        
        modelBuilder.Entity<Biere>()
            .Property(b => b.Prix)
            .HasColumnType("decimal(10,3)");
        
        modelBuilder.Entity<Biere>()
            .Property(b => b.DegresAlcool)
            .HasColumnType("decimal(10,3)");
        
        base.OnModelCreating(modelBuilder);
    }
}