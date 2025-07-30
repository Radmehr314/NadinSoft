using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Models.Products;
using NadinSoft.Domain.Models.Users;

namespace NadinSoft.Infrastructure.Persistance.SQl;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DataBaseContext(DbContextOptions options) : base(options) 
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}