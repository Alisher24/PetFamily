using Application.Extensions;
using Application.Volunteer.Requests;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonFields;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Volunteer.Services;

public class CreateVolunteerService(
    IVolunteerRepository volunteerRepository, 
    IValidator<CreateVolunteerRequest> validator,
    ILogger<CreateVolunteerRequest> logger)
{
    public async Task<Result<Guid>> ExecuteAsync(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var email = Email.Create(request.Email);

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);

        var volunteerEmail = await volunteerRepository
            .GetByEmailAsync(email.Value, cancellationToken);
        if (volunteerEmail.IsSuccess)
            return Errors.Volunteer.ValueIsAlreadyExists("email");

        var volunteerPhoneNumber = await volunteerRepository
            .GetByPhoneNumberAsync(phoneNumber.Value, cancellationToken);
        if (volunteerPhoneNumber.IsSuccess)
            return Errors.Volunteer.ValueIsAlreadyExists("phone number");
        
        var volunteerId = VolunteerId.NewVolunteerId();

        var fullName = FullName.Create(request.FullName.FirstName,
            request.FullName.LastName,
            request.FullName.Patronymic);

        var description = Description.Create(request.Description);

        var yearsExperience = YearsExperience.Create(request.YearsExperience);

        var socialNetworks = new SocialNetworkList(request.SocialNetworks
            .Select(s => new SocialNetwork(Name.Create(s.Name).Value,
                Link.Create(s.Link).Value)));

        var requisites = new RequisiteList(request.Requisites
            .Select(r => new Requisite(Name.Create(r.Name).Value,
                Description.Create(r.Description).Value)));

        var volunteer = new Domain.Aggregates.Volunteer.Volunteer(
            volunteerId,
            fullName.Value,
            email.Value,
            description.Value,
            yearsExperience.Value,
            phoneNumber.Value,
            socialNetworks,
            requisites);

        await volunteerRepository.AddAsync(volunteer, cancellationToken);

        logger.LogInformation("Created volunteer with id {volunteerId}", volunteerId.Value);

        return volunteer.Id.Value;
    }
}