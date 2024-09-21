using Domain.CommonValueObjects;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record Color : ValueObject<string>
{
    private Color(string value) : base(value)
    {
    }

    public static Result<Color> Create(string color)
    {
        if (string.IsNullOrWhiteSpace(color))
            return Errors.General.ValueIsInvalid("Color");

        return new Color(color);
    }
};