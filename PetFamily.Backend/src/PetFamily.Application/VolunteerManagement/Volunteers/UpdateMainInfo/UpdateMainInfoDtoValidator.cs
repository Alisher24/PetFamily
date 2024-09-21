using Application.Dtos;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonValueObjects;
using FluentValidation;

namespace Application.VolunteerManagement.Volunteers.UpdateMainInfo;

public class UpdateMainInfoDtoValidator : AbstractValidator<UpdateMainInfoDto>
{
    public UpdateMainInfoDtoValidator()
    {
        RuleFor(c => c.FullName).MustBeValueObject(f =>
            FullName.Create(f.FirstName, f.LastName, f.Patronymic));
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.YearsExperience).MustBeValueObject(YearsExperience.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
    }
}