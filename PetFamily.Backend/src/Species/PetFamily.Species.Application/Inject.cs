using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application;

public static class Inject
{
    public static IServiceCollection AddSpeciesApplication(this IServiceCollection services)
    {
        services
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes.AssignableToAny(typeof(ICommandService<,>), typeof(ICommandService<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryService<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}