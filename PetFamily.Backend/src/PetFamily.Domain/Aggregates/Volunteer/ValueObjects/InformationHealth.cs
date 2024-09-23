using Domain.CommonValueObjects;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record InformationHealth : ValueObject<string>
{
    private InformationHealth(string value) : base(value)
    {
    }

    public static Result<InformationHealth> Create(string informationHealth)
    {
        if (string.IsNullOrWhiteSpace(informationHealth))
            return Errors.General.ValueIsInvalid("Information Health");

        return new InformationHealth(informationHealth);
    }
};