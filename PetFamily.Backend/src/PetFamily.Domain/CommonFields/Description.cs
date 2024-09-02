﻿using Domain.Shared;

namespace Domain.CommonFields;

public record Description
{
    private Description()
    {
    }

    private Description(string value) => Value = value;
    public string Value { get; }

    public static Result<Description> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MaxHighTextLenth)
            return Errors.General.ValueIsInvalid("Description");

        return new Description(value);
    }
}