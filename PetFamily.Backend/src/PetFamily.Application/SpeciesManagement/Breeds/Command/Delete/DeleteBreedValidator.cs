using Domain.Shared;
using FluentValidation;

namespace Application.SpeciesManagement.Breeds.Command.Delete;

public class DeleteBreedValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedValidator()
    {
        RuleFor(d => d.SpeciesId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(d => d.BreedId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
    }
}