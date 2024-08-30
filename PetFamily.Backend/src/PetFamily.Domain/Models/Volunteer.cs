using CSharpFunctionalExtensions;
using Domain.Enums;
using Domain.Models.CommonFields;
using Domain.Models.ValueObjects;

namespace Domain.Models;

public class Volunteer: Shared.Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    
    // ef core
    private Volunteer(VolunteerId id) : base(id) { }

    private Volunteer(VolunteerId id,
        FullName fullName,
        string email,
        Description description,
        int yearsExperience,
        string phoneNumber) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsExperience = yearsExperience;
        PhoneNumber = phoneNumber;
    }
    public FullName FullName { get; private set; } = default!;
    
    public string Email { get; private set; } = default!;
    
    public Description Description { get; private set; } = default!;
    
    public int YearsExperience { get; private set; }
    
    public string PhoneNumber { get; private set; } = default!;

    public SocialNetworkList SocialNetworks { get; private set; }

    public AssistanceDetailList AssistanceDetails { get; private set; }
    
    public IReadOnlyList<Pet> Pets => _pets;

    public int PetsAdopted => _pets
        .Count(x => x.HelpStatus == HelpStatuses.FoundHouse);

    public int CurrentPetsSeekingHome => _pets
        .Count(x => x.HelpStatus == HelpStatuses.LookingForHome);

    public int PetsUnderTreatment => _pets
        .Count(x => x.HelpStatus == HelpStatuses.NeedsHelp);
    
    public void AddPet(Pet pet) => _pets.Add(pet);

    public static Result<Volunteer> Create(VolunteerId id,
        FullName fullName,
        string email,
        Description description,
        int yearsExperience,
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Volunteer>("Email cannot be empty");
        
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