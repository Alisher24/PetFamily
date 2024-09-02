using Domain.Aggregates.Species.Entities;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.CommonFields;
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
}