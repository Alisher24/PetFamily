using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Species.Application.Species.Commands.Delete;

public class DeleteSpeciesValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesValidator()
    {
        RuleFor(d => d.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid());
    }
}