using Application.Volunteer;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddSingleton<SoftDeleteInterceptor>();

        services.AddScoped<IVolunteerRepository, VolunteerRepository>();

        return services;
    }
}