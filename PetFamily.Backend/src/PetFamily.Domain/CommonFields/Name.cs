using Domain.Shared;

namespace Domain.CommonFields;

public record Name
{
    private Name()
    {
    }

    private Name(string value) => Value = value;

    public string Value { get; }

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MaxLowTextLenth)
            return Errors.General.ValueIsInvalid("Name");

        return new Name(value);
    }
}