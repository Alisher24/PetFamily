using Domain.CommonValueObjects;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record Weight : ValueObject<double>
{
    private Weight(double value) : base(value)
    {
    }

    public static Result<Weight> Create(double weight)
    {
        if (weight <= 0)
            return Errors.General.ValueIsInvalid("Weight");

        return new Weight(weight);
    }
};