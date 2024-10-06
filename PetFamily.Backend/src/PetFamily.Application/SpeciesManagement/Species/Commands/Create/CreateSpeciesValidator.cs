using Domain.CommonValueObjects;
using FluentValidation;

namespace Application.SpeciesManagement.Species.Commands.Create;

public class CreateSpeciesValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesValidator()
    {
        RuleFor(c => c.Name).MustBeValueObject(Name.Create);
        RuleFor(c => c.Description).MustBeValueObject(Description.Create);
    }
}