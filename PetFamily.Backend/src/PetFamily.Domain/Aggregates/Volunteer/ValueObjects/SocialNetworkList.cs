namespace Domain.Aggregates.Volunteer.ValueObjects;

public record SocialNetworkList
{
    private SocialNetworkList()
    {
    }

    public SocialNetworkList(IEnumerable<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks.ToList();
    }

    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
}