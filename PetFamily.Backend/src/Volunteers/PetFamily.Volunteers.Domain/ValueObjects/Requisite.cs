using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record Requisite
{
    //ef core
    private Requisite()
    {
    }

    public Requisite(Name name, Description description)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; } = default!;
    public Description Description { get; } = default!;
}