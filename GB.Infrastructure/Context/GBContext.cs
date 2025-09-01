using GB.Application.Interfaces;
using GB.Domain.Models;
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
    public DbSet<GrossisteBrasserie> GrossisteBrasseries { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GrossisteBrasserie>()
            .HasKey(gb => new { gb.GrossisteId, gb.BiereId });
        
        modelBuilder.Entity<GrossisteBrasserie>()
            .HasOne(gb => gb.Grossiste)
            .WithMany(g => g.GrossisteBrasseries)
            .HasForeignKey(gb => gb.GrossisteId);
        
        modelBuilder.Entity<GrossisteBrasserie>()
            .HasOne(gb => gb.Biere)
            .WithMany(b => b.GrossisteBrasseries)
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