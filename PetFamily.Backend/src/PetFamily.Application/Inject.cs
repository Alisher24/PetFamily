using Application.Volunteer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerService>();
        
        return services;
    }
}