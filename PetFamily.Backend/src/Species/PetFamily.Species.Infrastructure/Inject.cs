using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure.DbContexts;

namespace PetFamily.Species.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddSpeciesInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration)
            .AddRepositories()
            .AddServices();

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        return services.AddScoped<ISpeciesRepository, SpeciesRepository>();
    }
    
    private static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }


    private static IServiceCollection AddDbContexts(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }
}