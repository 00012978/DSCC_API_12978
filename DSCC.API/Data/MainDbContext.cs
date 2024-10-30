using DSCC.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace DSCC.API.Data;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pet>()
            .HasOne(e => e.Adoption)
            .WithOne(e => e.Pet)
            .HasForeignKey<Adoption>(e => e.Id)
            .IsRequired();
    }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Adoption> Adoptions { get; set; }
}
