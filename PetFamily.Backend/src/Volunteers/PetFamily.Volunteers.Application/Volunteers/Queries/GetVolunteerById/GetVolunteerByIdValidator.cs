using FluentValidation;

namespace PetFamily.Volunteers.Application.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdValidator : AbstractValidator<GetVolunteerByIdQuery>
{
    public GetVolunteerByIdValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
    }
}