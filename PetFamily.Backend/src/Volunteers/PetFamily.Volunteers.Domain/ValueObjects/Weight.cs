using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

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