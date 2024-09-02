using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Shared;

namespace Domain.Aggregates.Species.ValueObjects;

public record Type
{
    private Type() { }

    private Type(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public SpeciesId SpeciesId { get; } = default!;

    public Guid BreedId { get; }

    public static Result<Type> Create(SpeciesId speciesId, Guid breedId)
    {
        return new Type(speciesId, breedId);
    }
}