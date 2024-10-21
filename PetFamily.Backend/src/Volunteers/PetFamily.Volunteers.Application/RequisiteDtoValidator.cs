using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Dtos;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Application;

public class RequisiteDtoValidator : AbstractValidator<RequisiteDto>
{
    public RequisiteDtoValidator()
    {
        RuleFor(r => r.Name).MustBeValueObject(Name.Create);
        RuleFor(r => r.Description).MustBeValueObject(Description.Create);
    }
}