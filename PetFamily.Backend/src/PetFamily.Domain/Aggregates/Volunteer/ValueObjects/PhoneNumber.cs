﻿using System.Text.RegularExpressions;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record PhoneNumber : ValueObject<string>
{
    private const string PhoneNumberRegex = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

    private PhoneNumber(string value) : base(value)
    {
    }

    public static Result<PhoneNumber> Create(
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Errors.General.ValueIsInvalid("Phone number");

        if (phoneNumber.Length > Constants.MaxPhoneLenth)
            return Errors.General.ValueIsInvalid("Phone number");

        if (!Regex.IsMatch(phoneNumber, PhoneNumberRegex))
            return Errors.General.ValueIsInvalid("Phone number");

        return new PhoneNumber(phoneNumber);
    }
};