using CSharpFunctionalExtensions;
using Domain.Models.CommonFields;

namespace Domain.Models.Volunteer.ValueObjects;

public record Requisite
{
    private Requisite() { }
    
    private Requisite(Name name, Description description)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; } = default!;
    public Description Description { get; } = default!;

    public static Result<Requisite> Create(Name name, Description description)
    {
        return new Requisite(name, description);
    }
}