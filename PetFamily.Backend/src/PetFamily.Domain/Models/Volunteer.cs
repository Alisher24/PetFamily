using CSharpFunctionalExtensions;
using Domain.Enums;
using Domain.Models.Shared;

namespace Domain.Models;

public class Volunteer: Base.Entity<Guid>
{
    private readonly List<SocialNetwork> _socialNetworks = [];
    private readonly List<AssistanceDetail> _assistanceDetails = [];
    private readonly List<Pet> _pets = [];
    public FullName FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public int YearsExperience { get; private set; }
    public string PhoneNumber { get; private set; } = default!;
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<AssistanceDetail> AssistanceDetails => _assistanceDetails;
    public IReadOnlyList<Pet> Pets => _pets;

    public int PetsAdopted => _pets
        .Count(x => x.HelpStatus == HelpStatuses.FoundHouse);

    public int CurrentPetsSeekingHome => _pets
        .Count(x => x.HelpStatus == HelpStatuses.LookingForHome);

    public int PetsUnderTreatment => _pets
        .Count(x => x.HelpStatus == HelpStatuses.NeedsHelp);

    public void AddSocialNetwork(SocialNetwork socialNetwork) 
        => _socialNetworks.Add(socialNetwork);
    public void AddAssistanceDetail(AssistanceDetail assistanceDetail) 
        => _assistanceDetails.Add(assistanceDetail);
    public void AddPet(Pet pet) => _pets.Add(pet);
    
    // ef core
    private Volunteer(Guid id) : base(id) { }

    private Volunteer(Guid id,
        FullName fullName,
        string email,
        string description,
        int yearsExperience,
        string phoneNumber) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsExperience = yearsExperience;
        PhoneNumber = phoneNumber;
    }

    public static Result<Volunteer> Create(Guid id,
        FullName fullName,
        string email,
        string description,
        int yearsExperience,
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Volunteer>("Email cannot be empty");
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Volunteer>("Description cannot be empty");
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<Volunteer>("Phone number cannot be empty");

        return new Volunteer(id,
            fullName,
            email,
            description,
            yearsExperience,
            phoneNumber);
    }
}