using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record YearsExperience : ValueObject<int>
{
    private YearsExperience(int value) : base(value)
    {
    }

    public static Result<YearsExperience> Create(int yearsExperience)
    { 
        if (yearsExperience < 0)
            return Errors.General.ValueIsInvalid("YearsExperience");

        return new YearsExperience(yearsExperience);
    }
}