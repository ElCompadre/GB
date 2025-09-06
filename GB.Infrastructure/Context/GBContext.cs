using GB.Application.Interfaces;
using GB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GB.Infrastructure.Context;

public class GBContext(DbContextOptions<GBContext> options) : DbContext(options), IGBContext
{
    public DbSet<Brasserie> Brasseries { get; set; }
    public DbSet<Grossiste> Grossistes { get; set; }
    public DbSet<Biere> Bieres { get; set; }
    public DbSet<GrossisteBiere> GrossisteBieres { get; set; }
    public new void SaveChanges()
    {
        base.SaveChanges();
    }

    public new Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

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
            .HasColumnType("decimal(10,2)");
        
        modelBuilder.Entity<Biere>()
            .Property(b => b.DegresAlcool)
            .HasColumnType("decimal(10,2)");
        
        base.OnModelCreating(modelBuilder);
    }
}