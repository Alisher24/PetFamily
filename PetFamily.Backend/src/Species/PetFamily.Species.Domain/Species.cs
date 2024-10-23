using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.Species.Domain.Entities;

namespace PetFamily.Species.Domain;

public sealed class Species : Entity<SpeciesId>
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

    public void DeletePet(Breed breed) => _breeds.Remove(breed);
}