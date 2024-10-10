using Application.Dtos.Validators;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.Commands.UpdatePet;

public class UpdatePetValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid("VolunteerId"));
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsInvalid("PetId"));
        RuleFor(u => u.Name).MustBeValueObject(Name.Create);
        RuleFor(u => u.Description).MustBeValueObject(Description.Create);
        RuleFor(u => u.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsInvalid("SpeciesId"));
        RuleFor(u => u.BreedId).NotEmpty().WithError(Errors.General.ValueIsInvalid("BreedId"));
        RuleFor(u => u.Color).MustBeValueObject(Color.Create);
        RuleFor(u => u.InformationHealth).MustBeValueObject(InformationHealth.Create);
        RuleFor(u => u.Address).MustBeValueObject(a => Address.Create(a.City, a.District, a.Street, a.House));
        RuleFor(u => u.Weight).MustBeValueObject(Weight.Create);
        RuleFor(u => u.Height).MustBeValueObject(Height.Create);
        RuleFor(u => u.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(u => u.IsNeutered).NotEmpty().WithError(Errors.General.ValueIsAlreadyExists("IsNeutered"));
        RuleFor(u => u.DateOfBirth).MustBeValueObject(DateOfBirth.Create);
        RuleFor(u => u.IsVaccinated).NotEmpty().WithError(Errors.General.ValueIsAlreadyExists("IsVaccinated"));
        RuleFor(u => u.HelpStatus).NotEmpty().WithError(Errors.General.ValueIsAlreadyExists("HelpStatus"));
        RuleForEach(u => u.Requisites).SetValidator(new RequisiteDtoValidator());
    }
}