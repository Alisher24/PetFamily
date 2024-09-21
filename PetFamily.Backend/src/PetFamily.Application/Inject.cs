using Application.VolunteerManagement.Pets.AddPet;
using Application.VolunteerManagement.Pets.AddPetPhotos;
using Application.VolunteerManagement.Volunteers.Create;
using Application.VolunteerManagement.Volunteers.Delete;
using Application.VolunteerManagement.Volunteers.UpdateMainInfo;
using Application.VolunteerManagement.Volunteers.UpdateRequisites;
using Application.VolunteerManagement.Volunteers.UpdateSocialNetworks;
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
        services.AddScoped<AddPetService>();
        services.AddScoped<AddPetPhotosService>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}