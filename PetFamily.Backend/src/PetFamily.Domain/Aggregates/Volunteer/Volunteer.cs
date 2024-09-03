using Domain.Aggregates.Volunteer.Entities;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonFields;
using Domain.Enums;

namespace Domain.Aggregates.Volunteer;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];

    // ef core
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        YearsExperience yearsExperience,
        PhoneNumber phoneNumber,
        SocialNetworkList? socialNetworkList,
        RequisiteList? requisiteList) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsExperience = yearsExperience;
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworkList;
        Requisites = requisiteList;
    }

    public FullName FullName { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public YearsExperience YearsExperience { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; } = default!;

    public SocialNetworkList? SocialNetworks { get; private set; }

    public RequisiteList? Requisites { get; private set; }

    public IReadOnlyList<Pet> Pets => _pets;

    public int PetsAdopted => _pets
        .Count(x => x.HelpStatus == HelpStatuses.FoundHouse);

    public int CurrentPetsSeekingHome => _pets
        .Count(x => x.HelpStatus == HelpStatuses.LookingForHome);

    public int PetsUnderTreatment => _pets
        .Count(x => x.HelpStatus == HelpStatuses.NeedsHelp);

    public void AddPet(Pet pet) => _pets.Add(pet);
}