using CSharpFunctionalExtensions;
using Domain.Models.Fields;

namespace Domain.Models;

public class Breed : Shared.Entity<BreedId>
{
    //ef core
    private Breed(BreedId id) : base(id) { }

    private Breed(BreedId id, Name name, Description description) 
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public static Result<Breed> Create(BreedId id,
        Name name,
        Description description)
    {
        return new Breed(id, name, description);
    }
}