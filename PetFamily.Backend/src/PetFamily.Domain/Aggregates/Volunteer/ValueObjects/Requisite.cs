using Domain.CommonFields;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record Requisite
{
    private Requisite()
    {
    }

    public Requisite(Name name, Description description)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; } = default!;
    public Description Description { get; } = default!;
}