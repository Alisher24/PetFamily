using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;

namespace Application.SpeciesManagement.Breeds.Command;

public class AddBreedValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedValidator()
    {
        RuleFor(a => a.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsInvalid("SpeciesId"));
        RuleFor(a => a.Name).MustBeValueObject(Name.Create);
        RuleFor(a => a.Description).MustBeValueObject(Description.Create);
    }
}