using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Contracts;
using PetFamily.Volunteers.Infrastructure;

namespace PetFamily.Volunteers.Presentation;

public static class Inject
{
    public static IServiceCollection AddVolunteersModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IVolunteersContracts, VolunteersContracts>()
            .AddVolunteersInfrastructure(configuration)
            .AddVolunteersApplication();
    }
}
