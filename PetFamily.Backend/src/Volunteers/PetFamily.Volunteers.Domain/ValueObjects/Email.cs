using System.Text.RegularExpressions;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record Email : ValueObject<string>
{
    private const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    private Email(string value) : base(value)
    {
    }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Errors.General.ValueIsInvalid("Email");

        if (email.Length > Constants.MaxEmailLength)
            return Errors.General.ValueIsInvalid("Email");

        if (!Regex.IsMatch(email, EmailRegex, RegexOptions.IgnoreCase))
            return Errors.General.ValueIsInvalid("Email");

        return new Email(email);
    }
}