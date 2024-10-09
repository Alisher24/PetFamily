using Domain.Shared;
using FluentValidation;

namespace Application.SpeciesManagement.Species.Commands.Delete;

public class DeleteSpeciesValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesValidator()
    {
        RuleFor(d => d.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid());
    }
}