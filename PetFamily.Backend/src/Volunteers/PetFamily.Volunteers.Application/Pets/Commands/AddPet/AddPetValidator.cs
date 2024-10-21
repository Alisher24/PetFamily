using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Pets.Commands.AddPet;

public class AddPetValidator : AbstractValidator<AddPetCommand>
{
    public AddPetValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.Name).MustBeValueObject(Name.Create);
        RuleFor(a => a.Description).MustBeValueObject(Description.Create);
        RuleFor(a => a.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.BreedId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.Color).MustBeValueObject(Color.Create);
        RuleFor(a => a.InformationHealth).MustBeValueObject(InformationHealth.Create);
        RuleFor(a => a.Address).MustBeValueObject(a =>
            Address.Create(a.City, a.District, a.Street, a.House));
        RuleFor(a => a.Weight).MustBeValueObject(Weight.Create);
        RuleFor(a => a.Height).MustBeValueObject(Height.Create);
        RuleFor(a => a.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(a => a.IsNeutered).NotNull().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.DateOfBirth).MustBeValueObject(DateOfBirth.Create);
        RuleFor(a => a.IsVaccinated).NotNull().WithError(Errors.General.ValueIsInvalid());
        RuleFor(a => a.HelpStatus).NotEmpty()
            .Must(h => Enum.TryParse<HelpStatuses>(h, out _))
            .WithError(Errors.General.ValueIsInvalid("HelpStatus"));
    }
}