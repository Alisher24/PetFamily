using CSharpFunctionalExtensions;

namespace Domain.Models.ValueObjects;

public record PetDetails
{
    private PetDetails() { }

    private PetDetails(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public SpeciesId SpeciesId { get; } = default!;

    public Guid BreedId { get; }

    public static Result<PetDetails> Create(SpeciesId speciesId, Guid breedId)
    {
        return new PetDetails(speciesId, breedId);
    }
}