using PetFamily.SharedKernel.Shared;

namespace PetFamily.SharedKernel.ValueObjects;

public record Name
{
    private Name()
    {
    }

    private Name(string value) => Value = value;

    public string Value { get; }

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Name");
        if (value.Length > Constants.MaxLowTextLength)
            return Errors.General.ValueIsInvalid("Name");

        return new Name(value);
    }
}