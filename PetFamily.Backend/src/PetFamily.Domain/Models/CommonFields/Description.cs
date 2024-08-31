using CSharpFunctionalExtensions;
using Domain.Models.Shared;

namespace Domain.Models.CommonFields;

public record Description 
{
    private Description() {}
    private Description(string value) => Value = value;
    public string Value { get; }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MAX_HIGH_TEXT_LENTH)
            return Errors.General.ValueIsInvalid("Description");

        return new Description(value);
    }
}