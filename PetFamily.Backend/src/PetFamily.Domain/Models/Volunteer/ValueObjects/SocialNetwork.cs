using CSharpFunctionalExtensions;
using Domain.Models.CommonFields;
using Domain.Models.Shared;

namespace Domain.Models.Volunteer.ValueObjects;

public record SocialNetwork
{
    private SocialNetwork() { }
    
    private SocialNetwork(Name name, string link)
    {
        Name = name;
        Link = link;
    }

    public Name Name { get; } = default!;
    public string Link { get; } = default!;

    public static Result<SocialNetwork, Error> Create(Name name, string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsInvalid("Link");

        return new SocialNetwork(name, link);
    }
}