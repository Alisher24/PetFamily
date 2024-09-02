using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record Link : ValueObject<string>
{
    private Link(string value) : base(value)
    {
    }

    public static Result<Link> Create(string link)
    {
        link = link.Trim();
        if (string.IsNullOrWhiteSpace(link)
            || link.Contains(' ')
            || link.Length > Constants.MaxHighTextLenth)
        {
            return Errors.General.ValueIsInvalid("Link");
        }

        return new Link(link);
    }
}