using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record DateOfBirth : ValueObject<DateOnly>
{
    private DateOfBirth(DateOnly value) : base(value)
    {
    }

    public static Result<DateOfBirth> Create(DateOnly dateOfBirth)
    {
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now.AddDays(1)))
            return Errors.General.ValueIsInvalid("DateOfBirth");

        return new DateOfBirth(dateOfBirth);
    }
};