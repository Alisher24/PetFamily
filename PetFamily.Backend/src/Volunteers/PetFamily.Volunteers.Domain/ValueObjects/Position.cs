using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record Position : ValueObject<int>
{
    private Position(int value) : base(value)
    {
    }

    public static Result<Position> Create(int serialNumber)
    {
        if (serialNumber <= 0)
            return Errors.General.ValueIsInvalid("serial number");

        return new Position(serialNumber);
    }

    public static implicit operator int(Position position) =>
        position.Value;
}