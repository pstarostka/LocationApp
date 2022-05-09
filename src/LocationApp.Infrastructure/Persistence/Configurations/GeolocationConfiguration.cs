using LocationApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocationApp.Infrastructure.Persistence.Configurations;

public class GeolocationConfiguration : IEntityTypeConfiguration<GeolocationEntity>
{
    public void Configure(EntityTypeBuilder<GeolocationEntity> builder)
    {
        // tbd
    }
}