using CSharpFunctionalExtensions;

namespace Domain.Models.ValueObjects;

public record SocialNetwork
{
    private SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }

    public string Name { get; } = default!;
    public string Link { get; } = default!;

    public static Result<SocialNetwork> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<SocialNetwork>("Name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(link))
            return Result.Failure<SocialNetwork>("Link cannot be empty");

        return new SocialNetwork(name, link);
    }
}