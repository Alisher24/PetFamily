using Domain.CommonValueObjects;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record SocialNetwork
{
    //ef core
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