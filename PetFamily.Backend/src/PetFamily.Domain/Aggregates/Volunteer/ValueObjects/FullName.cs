using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record FullName
{
    private FullName(string firstName, string lastName, string? patronymic)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? Patronymic { get; }

    public static Result<FullName> Create(string firstName, string lastName, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsInvalid("First name");
        if (firstName.Length > Constants.MaxLowTextLenth)
            return Errors.General.ValueIsInvalid("First name");

        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsInvalid("Last name");
        if (lastName.Length > Constants.MaxLowTextLenth)
            return Errors.General.ValueIsInvalid("Last name");

        if (!string.IsNullOrWhiteSpace(patronymic) && patronymic.Length > Constants.MaxLowTextLenth)
            return Errors.General.ValueIsInvalid("Patronymic");

        return new FullName(firstName, lastName, patronymic);
    }
}