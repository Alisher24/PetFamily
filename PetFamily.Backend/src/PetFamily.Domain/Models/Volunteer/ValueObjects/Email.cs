﻿using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Domain.Models.Shared;

namespace Domain.Models.Volunteer.ValueObjects;

public record Email : ValueObject<string>
{
    private const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    
    private Email(string value) : base(value) { }

    public static Result<Email, Error> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email)
            || email.Length > Constants.MAX_EMAIL_LENTH
            || !Regex.IsMatch(email, EmailRegex, RegexOptions.IgnoreCase))
        {
            return Errors.General.ValueIsInvalid("Email");
        }

        return new Email(email);
    }
}