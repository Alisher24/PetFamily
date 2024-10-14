using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Pets.Queries.GetPetById;

public class GetPetByIdValidator : AbstractValidator<GetPetByIdQuery>
{
    public GetPetByIdValidator()
    {
        RuleFor(g => g.Id).NotEmpty().WithError(Errors.General.ValueIsInvalid("Id"));
    }
}