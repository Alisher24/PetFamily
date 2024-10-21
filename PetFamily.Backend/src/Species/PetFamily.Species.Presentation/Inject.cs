using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure;

namespace PetFamily.Species.Presentation;

public static class Inject
{
    public static IServiceCollection AddSpeciesModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSpeciesInfrastructure(configuration)
            .AddSpeciesApplication();
    }
}