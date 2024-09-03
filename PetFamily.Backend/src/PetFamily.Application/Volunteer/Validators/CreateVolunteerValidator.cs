﻿using Application.Volunteer.Requests;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.CommonFields;
using FluentValidation;

namespace Application.Volunteer.Validators;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.FullName).MustBeValueObject(f =>
            FullName.Create(f.FirstName, f.LastName, f.Patronymic));

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);

        RuleFor(c => c.Description).MustBeValueObject(Description.Create);

        RuleFor(c => c.YearsExperience).MustBeValueObject(YearsExperience.Create);

        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleForEach(c => c.SocialNetworks).MustBeValueObject(s =>
            Name.Create(s.Name));

        RuleForEach(c => c.SocialNetworks).MustBeValueObject(s =>
            Link.Create(s.Link));

        RuleForEach(c => c.Requisites).MustBeValueObject(r =>
            Name.Create(r.Name));

        RuleForEach(c => c.Requisites).MustBeValueObject(r =>
            Description.Create(r.Description));
    }
}