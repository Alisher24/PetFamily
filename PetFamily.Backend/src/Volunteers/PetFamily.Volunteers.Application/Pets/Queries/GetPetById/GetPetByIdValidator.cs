using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Pets.Queries.GetPetById;

public class GetPetByIdValidator : AbstractValidator<GetPetByIdQuery>
{
    public GetPetByIdValidator()
    {
        RuleFor(g => g.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid("Id"));
    }
}