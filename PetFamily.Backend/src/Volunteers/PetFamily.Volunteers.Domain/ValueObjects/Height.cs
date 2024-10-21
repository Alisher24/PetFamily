using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record Height : ValueObject<double>
{
    private Height(double value) : base(value)
    {
    }

    public static Result<Height> Create(double height)
    {
        if (height <= 0)
            return Errors.General.ValueIsInvalid("Height");

        return new Height(height);
    }
};