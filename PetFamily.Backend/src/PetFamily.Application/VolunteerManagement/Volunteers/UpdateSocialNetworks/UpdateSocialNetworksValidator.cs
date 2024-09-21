using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonValueObjects;
using Domain.Shared;
using FluentValidation;

namespace Application.VolunteerManagement.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworksValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialNetworksValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleForEach(u => u.SocialNetworks).MustBeValueObject(s =>
            Name.Create(s.Name));
        RuleForEach(c => c.SocialNetworks).MustBeValueObject(s =>
            Link.Create(s.Link));
    }
}