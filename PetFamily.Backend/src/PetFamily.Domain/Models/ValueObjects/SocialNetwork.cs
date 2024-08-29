using CSharpFunctionalExtensions;
using Domain.Models.Fields;

namespace Domain.Models.ValueObjects;

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

    public static Result<SocialNetwork> Create(Name name, string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return Result.Failure<SocialNetwork>("Link cannot be empty");

        return new SocialNetwork(name, link);
    }
}