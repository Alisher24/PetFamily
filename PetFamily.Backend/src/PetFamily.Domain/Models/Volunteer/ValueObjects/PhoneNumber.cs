using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Domain.Models.Shared;

namespace Domain.Models.Volunteer.ValueObjects;

public record PhoneNumber : ValueObject<string>
{
    private const string PhoneNumberRegex = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
    
    private PhoneNumber(string value) : base(value) { }

    public static Result<PhoneNumber, Error> Create(
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)
            || phoneNumber.Length > Constants.MAX_PHONE_LENTH
            || !Regex.IsMatch(phoneNumber, PhoneNumberRegex))
        {
            return Errors.General.ValueIsInvalid("Phone number");
        }

        return new PhoneNumber(phoneNumber);
    }
};