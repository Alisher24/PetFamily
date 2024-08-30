using CSharpFunctionalExtensions;

namespace Domain.Models.ValueObjects;

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