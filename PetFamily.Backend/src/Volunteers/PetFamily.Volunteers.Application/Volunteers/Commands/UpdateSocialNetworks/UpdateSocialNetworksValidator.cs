﻿using FluentValidation;
using PetFamily.Core;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.UpdateSocialNetworks;

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