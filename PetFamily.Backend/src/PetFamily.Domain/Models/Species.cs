using CSharpFunctionalExtensions;
using Domain.Models.CommonFields;

namespace Domain.Models;

public class Species : Shared.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    
    // ef core
    private Species(SpeciesId id) : base(id) { }

    private Species(SpeciesId id, Name name, Description description)
        :base(id)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public IReadOnlyList<Breed> Breeds => _breeds;

    public void AddBread(Breed breed) => _breeds.Add(breed);

    public static Result<Species> Create(SpeciesId id, 
        Name name, 
        Description description)
    {
        return new Species(id, name, description);
    }
}