using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.DB;

public class ResturantDBContext : DbContext
{
    public ResturantDBContext(DbContextOptions<ResturantDBContext> options) : base(options)
    {
    }

    public DbSet<Resturant> Resturants { get; set; }
    public DbSet<ResturantTable> ResturantTable { get; set; }
    public DbSet<ResturantOwner> ResturantOwner { get; set; }
    public DbSet<UserBookings> UserBookings { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ResturantDBContext).Assembly);
    }
}
