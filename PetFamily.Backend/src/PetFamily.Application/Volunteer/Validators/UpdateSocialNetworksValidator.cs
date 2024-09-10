using Application.Volunteer.Requests;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonFields;
using Domain.Shared;
using FluentValidation;

namespace Application.Volunteer.Validators;

public class UpdateSocialNetworksValidator : AbstractValidator<UpdateSocialNetworksRequest>
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