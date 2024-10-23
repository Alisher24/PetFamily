using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.Create;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.FullName).MustBeValueObject(f =>
            FullName.Create(f.FirstName, f.LastName, f.Patronymic));
        RuleFor(c => c.Email).MustBeValueObject(Email.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        RuleFor(c => c.YearsExperience).MustBeValueObject(YearsExperience.Create);
        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleForEach(c => c.SocialNetworks).MustBeValueObject(s =>
            Name.Create(s.Name));
        RuleForEach(c => c.SocialNetworks).MustBeValueObject(s =>
            Link.Create(s.Link));
        RuleForEach(c => c.Requisites).MustBeValueObject(r =>
            Name.Create(r.Name));
        RuleForEach(c => c.Requisites).MustBeValueObject(r =>
            Description.Create(r.Description));
    }
}