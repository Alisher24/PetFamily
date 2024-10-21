using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;

namespace PetFamily.Species.Domain.Entities;

public class Breed : Entity<BreedId>
{
    //ef core
    private Breed(BreedId id) : base(id)
    {
    }

    public Breed(BreedId id, Name name, Description description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;
}