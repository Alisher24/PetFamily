using Application.Volunteer.Dto;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonFields;
using FluentValidation;

namespace Application.Volunteer.Validators;

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