using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record Link : ValueObject<string>
{
    private Link(string value) : base(value)
    {
    }

    public static Result<Link> Create(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsInvalid("Link");

        if (link.Contains(' '))
            return Errors.General.ValueIsInvalid("Link");

        if (link.Length > Constants.MaxHighTextLength)
            return Errors.General.ValueIsInvalid("Link");

        return new Link(link);
    }
}