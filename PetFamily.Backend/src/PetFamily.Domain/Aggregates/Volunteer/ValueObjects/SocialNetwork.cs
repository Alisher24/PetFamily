using Domain.CommonFields;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record SocialNetwork
{
    private SocialNetwork()
    {
    }

    public SocialNetwork(Name name, Link link)
    {
        Name = name;
        Link = link;
    }

    public Name Name { get; } = default!;
    public Link Link { get; } = default!;
}