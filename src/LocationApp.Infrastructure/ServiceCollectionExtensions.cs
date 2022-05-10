using System.Reflection;
using LocationApp.Application.Interfaces;
using LocationApp.Infrastructure.Persistence;
using LocationApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace LocationApp.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var ipStackUrl = configuration.GetSection("IpStackUrl").Value ?? throw new Exception("IpStackUrl is missing");
        services.AddSingleton<RestClient>(x => new RestClient(ipStackUrl));
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IGeolocationService, IpStackGeolocationService>();
        services.Decorate<IGeolocationService, GeolocationPersistenceService>();
        services.AddDbContext<InMemoryDbContext>();

        var dbContext = services.BuildServiceProvider().GetRequiredService<InMemoryDbContext>();
        dbContext.Database.EnsureCreated();
        InMemoryDbContextSeed.SeedData(dbContext);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}