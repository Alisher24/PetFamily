using Domain.CommonValueObjects;
using FluentValidation;

namespace Application.Dtos.Validators;

public class RequisiteDtoValidator : AbstractValidator<RequisiteDto>
{
    public RequisiteDtoValidator()
    {
        RuleFor(r => r.Name).MustBeValueObject(Name.Create);
        RuleFor(r => r.Description).MustBeValueObject(Description.Create);
    }
}