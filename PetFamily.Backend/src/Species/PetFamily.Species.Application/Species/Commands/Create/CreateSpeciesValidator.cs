using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Species.Application.Species.Commands.Create;

public class CreateSpeciesValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesValidator()
    {
        RuleFor(c => c.Name).MustBeValueObject(Name.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
    }
}