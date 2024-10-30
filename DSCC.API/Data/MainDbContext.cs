using DSCC.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC.API.Data;

public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {

    }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Adoption> Adoptions { get; set; }
}
