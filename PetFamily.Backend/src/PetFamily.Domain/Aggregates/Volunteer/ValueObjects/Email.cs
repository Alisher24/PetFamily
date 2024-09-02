﻿using System.Text.RegularExpressions;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record Email : ValueObject<string>
{
    private const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    private Email(string value) : base(value)
    {
    }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email)
            || email.Length > Constants.MaxEmailLenth
            || !Regex.IsMatch(email, EmailRegex, RegexOptions.IgnoreCase))
        {
            return Errors.General.ValueIsInvalid("Email");
        }

        return new Email(email);
    }
}