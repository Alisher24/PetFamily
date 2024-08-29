using CSharpFunctionalExtensions;

namespace Domain.Models.ValueObjects;

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
            return Result.Failure<FullName>("First name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<FullName>("Last name cannot be empty");

        return new FullName(firstName, lastName, patronymic);
    }
}