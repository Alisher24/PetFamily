using FluentValidation;
using PetFamily.Core;
using PetFamily.Core.Dtos.Filters;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Pets.Queries.GetPetsWithPaginationAndFiltering;

public class PetsFilterDtoValidator : AbstractValidator<PetsFilterDto>
{
    public PetsFilterDtoValidator()
    {
        RuleFor(p => p.Name).Must(n => n is null || Name.Create(n).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("Name"));
        RuleFor(p => p.Description).Must(d => d is null || Description.Create(d).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("Description"));
        RuleFor(p => p.Color).Must(c => c is null || Color.Create(c).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("Color"));
        RuleFor(p => p.InformationHealth).Must(i => i is null || InformationHealth.Create(i).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("InformationHealth"));
        RuleFor(p => p.WeightFrom).Must(w => w is null || Weight.Create((double)w).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("WeightFrom"));
        RuleFor(p => p.WeightTo).Must(w => w is null || Weight.Create((double)w).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("WeightTo"));
        RuleFor(p => p.HeightFrom).Must(h => h is null || Height.Create((double)h).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("HeightFrom"));
        RuleFor(p => p.HeightTo).Must(h => h is null || Height.Create((double)h).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("HeightTo"));
        RuleFor(p => p.PhoneNumber).Must(p => p is null || PhoneNumber.Create(p).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("PhoneNumber"));
        RuleFor(p => p.DateOfBirthFrom).Must(d => d is null || DateOfBirth.Create((DateOnly)d).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("DateOfBirthFrom"));
        RuleFor(p => p.DateOfBirthTo).Must(d => d is null || DateOfBirth.Create((DateOnly)d).IsSuccess)
            .WithError(Errors.General.ValueIsInvalid("DateOfBirthTo"));
        RuleFor(p => p.HelpStatus).Must(h => h is null || Enum.TryParse<HelpStatuses>(h, out _))
            .WithError(Errors.General.ValueIsInvalid("HelpStatus"));
        RuleFor(p => p.CreatedAtFrom).Must(c => c is null || c <= DateTime.UtcNow)
            .WithError(Errors.General.ValueIsInvalid("CreatedAtFrom"));
        RuleFor(p => p.CreatedAtTo).Must(c => c is null || c <= DateTime.UtcNow)
            .WithError(Errors.General.ValueIsInvalid("CreatedAtTo"));
        RuleFor(p => p.PositionFrom).Must(p => p is null or > 0)
            .WithError(Errors.General.ValueIsInvalid("PositionFrom"));
        RuleFor(p => p.PositionTo).Must(p => p is null or > 0)
            .WithError(Errors.General.ValueIsInvalid("PositionTo"));
    }
}