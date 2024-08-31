namespace Domain.Models.Volunteer.ValueObjects.Ids;

public record PetId : ValueObject<Guid>
{
    private PetId(Guid value) : base(value) { }

    public static PetId NewPetId() => new(Guid.NewGuid());

    public static PetId Empty() => new(Guid.Empty);
    
    public static PetId Create(Guid id) => new(id);
}