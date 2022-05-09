using System.Reflection;
using LocationApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocationApp.Infrastructure.Persistence;

public class SqLiteDbContext : DbContext
{
    public DbSet<GeolocationEntity> Geolocations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}