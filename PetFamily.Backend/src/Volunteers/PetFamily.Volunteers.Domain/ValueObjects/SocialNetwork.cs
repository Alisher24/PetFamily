using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

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