using Domain.CommonValueObjects;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

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