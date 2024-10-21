using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.Create;

public class CreateVolunteerService(
    IVolunteerRepository volunteerRepository, 
    IValidator<CreateVolunteerCommand> validator,
    ILogger<CreateVolunteerService> logger,
    IUnitOfWork unitOfWork) : ICommandService<Guid, CreateVolunteerCommand>
{
    public async Task<Result<Guid>> ExecuteAsync(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var email = Email.Create(command.Email);

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber);

        var volunteerEmail = await volunteerRepository
            .GetByEmailAsync(email.Value, cancellationToken);
        if (volunteerEmail.IsSuccess)
            return Errors.General.ValueIsAlreadyExists("email");

        var volunteerPhoneNumber = await volunteerRepository
            .GetByPhoneNumberAsync(phoneNumber.Value, cancellationToken);
        if (volunteerPhoneNumber.IsSuccess)
            return Errors.General.ValueIsAlreadyExists("phone number");
        
        var volunteerId = VolunteerId.NewVolunteerId();

        var fullName = FullName.Create(command.FullName.FirstName,
            command.FullName.LastName,
            command.FullName.Patronymic);

        var description = Description.Create(command.Description);

        var yearsExperience = YearsExperience.Create(command.YearsExperience);

        var socialNetworks = new List<SocialNetwork>(command.SocialNetworks
            .Select(s => new SocialNetwork(Name.Create(s.Name).Value,
                Link.Create(s.Link).Value)));

        var requisites = new List<Requisite>(command.Requisites
            .Select(r => new Requisite(Name.Create(r.Name).Value,
                Description.Create(r.Description).Value)));

        var volunteer = new Volunteer(
            volunteerId,
            fullName.Value,
            email.Value,
            description.Value,
            yearsExperience.Value,
            phoneNumber.Value,
            socialNetworks,
            requisites);

        await volunteerRepository.AddAsync(volunteer, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created volunteer with id {volunteerId}", volunteerId.Value);

        return volunteer.Id.Value;
    }
}