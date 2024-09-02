using Domain.Aggregates.Species.ValueObjects.Ids;

namespace Domain.Aggregates.Species.ValueObjects;

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