﻿using Application.TestMinio.Services;
using Application.Volunteer.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerService>();
        services.AddScoped<UpdateMainInfoService>();
        services.AddScoped<UpdateSocialNetworksService>();
        services.AddScoped<UpdateRequisitesService>();
        services.AddScoped<DeleteVolunteerService>();
        services.AddScoped<UploadTestService>();
        services.AddScoped<GetTestService>();
        services.AddScoped<GetAllTestService>();
        services.AddScoped<DeleteTestService>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}