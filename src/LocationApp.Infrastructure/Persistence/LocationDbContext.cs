using System.Reflection;
using LocationApp.Application.Interfaces;
using LocationApp.Domain.Common;
using LocationApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LocationApp.Infrastructure.Persistence;

internal class LocationDbContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<LocationDbContext> _logger;

    public LocationDbContext(IDateTimeProvider dateTimeProvider, ILogger<LocationDbContext> logger)
    {
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public DbSet<GeolocationEntity> Geolocations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase("GeolocationDB");

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _dateTimeProvider.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = _dateTimeProvider.UtcNow;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Successfully saved {Count} entries", result);
        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}