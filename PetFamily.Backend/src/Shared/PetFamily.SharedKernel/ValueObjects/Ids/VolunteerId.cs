﻿namespace PetFamily.SharedKernel.ValueObjects.Ids;

public record VolunteerId : ValueObject<Guid>
{
    private VolunteerId(Guid value) : base(value) { }

    public static VolunteerId NewVolunteerId() => new(Guid.NewGuid());

    public static VolunteerId Empty() => new(Guid.Empty);

    public static VolunteerId Create(Guid id) => new(id);

    public static implicit operator VolunteerId(Guid id) => new(id);

    public static implicit operator Guid(VolunteerId volunteerId)
    {
        ArgumentNullException.ThrowIfNull(volunteerId);
        return volunteerId.Value;
    }
}