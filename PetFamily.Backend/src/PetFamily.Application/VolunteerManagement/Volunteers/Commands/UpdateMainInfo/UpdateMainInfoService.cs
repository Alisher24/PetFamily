using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VolunteerManagement.Volunteers.Commands.UpdateMainInfo;

public class UpdateMainInfoService(
    IVolunteerRepository volunteerRepository,
    IValidator<UpdateMainInfoCommand> validator,
    ILogger<UpdateMainInfoService> logger,
    IUnitOfWork unitOfWork) : ICommandService<Guid, UpdateMainInfoCommand>
{
    public async Task<Result<Guid>> ExecuteAsync(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");

        var email = Email.Create(command.Dto.Email).Value;

        var phoneNumber = PhoneNumber.Create(command.Dto.PhoneNumber).Value;

        if (volunteerResult.Value.Email.Value != command.Dto.Email)
        {
            var volunteerEmail = await volunteerRepository
                .GetByEmailAsync(email, cancellationToken);
            if (volunteerEmail.IsSuccess)
                return Errors.General.ValueIsAlreadyExists("email");
        }

        if (volunteerResult.Value.PhoneNumber.Value != command.Dto.PhoneNumber)
        {
            var volunteerPhoneNumber = await volunteerRepository
                .GetByPhoneNumberAsync(phoneNumber, cancellationToken);
            if (volunteerPhoneNumber.IsSuccess)
                return Errors.General.ValueIsAlreadyExists("phone number");
        }

        var fullName = FullName.Create(command.Dto.FullName.FirstName,
            command.Dto.FullName.LastName,
            command.Dto.FullName.Patronymic).Value;

        var description = Description.Create(command.Dto.Description).Value;

        var yearsExperience = YearsExperience.Create(command.Dto.YearsExperience).Value;

        volunteerResult.Value.UpdateMainInfo(fullName, email, description, yearsExperience, phoneNumber);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated volunteer with id {volunteerId}", command.VolunteerId);

        return volunteerResult.Value.Id.Value;
    }
}