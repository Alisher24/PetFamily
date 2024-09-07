using Application.Volunteer.Requests;
using Domain.CommonFields;
using FluentValidation;

namespace Application.Volunteer.Validators;

public class UpdateRequisitesValidator : AbstractValidator<UpdateRequisitesRequest>
{
    public UpdateRequisitesValidator()
    {
        RuleForEach(u => u.Requisites).MustBeValueObject(r =>
            Name.Create(r.Name));
        RuleForEach(u => u.Requisites).MustBeValueObject(r =>
            Description.Create(r.Description));
    }
}