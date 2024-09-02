using Application.Volunteer.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerService>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}