using Domain.CommonValueObjects;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record SerialNumber : ValueObject<int>
{
    private SerialNumber(int value) : base(value)
    {
    }

    public static Result<SerialNumber> Create(int serialNumber)
    {
        if (serialNumber <= 0)
            return Errors.General.ValueIsInvalid("serial number");

        return new SerialNumber(serialNumber);
    }
}