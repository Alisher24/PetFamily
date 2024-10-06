using FluentValidation;

namespace Application.VolunteerManagement.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdValidator : AbstractValidator<GetVolunteerByIdQuery>
{
    public GetVolunteerByIdValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
    }
}