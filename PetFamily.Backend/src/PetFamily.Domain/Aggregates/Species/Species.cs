using Domain.Aggregates.Species.Entities;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Shared;

namespace Domain.Aggregates.Species;

public class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    // ef core
    private Species(SpeciesId id) : base(id)
    {
    }

    public Species(SpeciesId id, Name name, Description description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public IReadOnlyList<Breed> Breeds => _breeds;

    public void AddBread(Breed breed) => _breeds.Add(breed);

    public Result<Breed> GetBreedById(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == breedId);
        if (breed is null)
            return Errors.General.NotFound($"Breed with id: {breedId.Value}");

        return breed;
    }

    public Result<Breed> GetBreedByName(Name name)
    {
        var breed = _breeds.FirstOrDefault(b => b.Name == name);
        if (breed is null)
            return Errors.General.NotFound($"Breed with name: {name.Value}");

        return breed;
    }
}