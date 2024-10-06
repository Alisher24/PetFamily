using Domain.CommonValueObjects;
using FluentValidation;

namespace Application.VolunteerManagement.Volunteers.Commands.UpdateRequisites;

public class UpdateRequisitesValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisitesValidator()
    {
        RuleForEach(u => u.Requisites).MustBeValueObject(r =>
            Name.Create(r.Name));
        RuleForEach(u => u.Requisites).MustBeValueObject(r =>
            Description.Create(r.Description));
    }
}