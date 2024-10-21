using PetFamily.SharedKernel.ValueObjects.Ids;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record Type
{
    private Type() { }

    public Type(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public SpeciesId SpeciesId { get; } = default!;

    public Guid BreedId { get; }
}