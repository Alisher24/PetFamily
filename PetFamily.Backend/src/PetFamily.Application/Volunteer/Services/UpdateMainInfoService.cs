using Application.Extensions;
using Application.Volunteer.Requests;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonFields;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Volunteer.Services;

public class UpdateMainInfoService(
    IVolunteerRepository volunteerRepository,
    IValidator<UpdateMainInfoRequest> validator,
    ILogger<UpdateMainInfoRequest> logger)
{
    public async Task<Result<Guid>> ExecuteAsync(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(request.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {request.VolunteerId}");

        var email = Email.Create(request.Dto.Email).Value;

        var phoneNumber = PhoneNumber.Create(request.Dto.PhoneNumber).Value;

        if (volunteerResult.Value.Email.Value != request.Dto.Email)
        {
            var volunteerEmail = await volunteerRepository
                .GetByEmailAsync(email, cancellationToken);
            if (volunteerEmail.IsSuccess)
                return Errors.Volunteer.ValueIsAlreadyExists("email");
        }

        if (volunteerResult.Value.PhoneNumber.Value != request.Dto.PhoneNumber)
        {
            var volunteerPhoneNumber = await volunteerRepository
                .GetByPhoneNumberAsync(phoneNumber, cancellationToken);
            if (volunteerPhoneNumber.IsSuccess)
                return Errors.Volunteer.ValueIsAlreadyExists("phone number");
        }

        var fullName = FullName.Create(request.Dto.FullName.FirstName,
            request.Dto.FullName.LastName,
            request.Dto.FullName.Patronymic).Value;

        var description = Description.Create(request.Dto.Description).Value;

        var yearsExperience = YearsExperience.Create(request.Dto.YearsExperience).Value;

        volunteerResult.Value.UpdateMainInfo(fullName, email, description, yearsExperience, phoneNumber);

        var result = await volunteerRepository.SaveAsync(volunteerResult.Value, cancellationToken);

        logger.LogInformation("Updated volunteer with id {volunteerId}", request.VolunteerId);

        return result;
    }
}