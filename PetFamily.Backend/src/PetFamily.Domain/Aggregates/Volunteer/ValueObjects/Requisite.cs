using Domain.CommonFields;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record Requisite
{
    private Requisite()
    {
    }

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