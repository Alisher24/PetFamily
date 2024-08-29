using CSharpFunctionalExtensions;
using Domain.Enums;
using Domain.Models.ValueObjects;

namespace Domain.Models;

public class Pet : Shared.Entity<PetId>
{
    // ef core
    private Pet(PetId id) : base(id) { }
    private Pet(PetId id,
        string nickname,
        string description,
        string breed,
        string color,
        string informationHealth,
        Address address,
        double weight,
        double height,
        string contactPhoneNumber,
        bool isNeutered,
        DateOnly dateOfBirth,
        bool isVaccinated,
        HelpStatuses helpStatus) : base(id)
    {
        Nickname = nickname;
        Description = description;
        Breed = breed;
        Color = color;
        InformationHealth = informationHealth;
        Address = address;
        Weight = weight;
        Height = height;
        ContactPhoneNumber = contactPhoneNumber;
        IsNeutered = isNeutered;
        DateOfBirth = dateOfBirth;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
        CreatedAt = DateTime.Now;
    }
    public string Nickname { get; private set; } = default!;
    
    public string Description { get; private set; } = default!;
    
    public string Breed { get; private set; } = default!;
    
    public string Color { get; private set; } = default!;
    
    public string InformationHealth { get; private set; } = default!;
    
    public Address Address { get; private set; } = default!;
    
    public double Weight { get; private set; }
    
    public double Height { get; private set; }
    
    public string ContactPhoneNumber { get; private set; } = default!;
    
    public bool IsNeutered { get; private set; }
    
    public DateOnly DateOfBirth { get; private set; }
    
    public bool IsVaccinated { get; private set; }
    
    public HelpStatuses HelpStatus { get; private set; }
    
    public DateTime CreatedAt { get; private set; }

    public AssistanceDetailList AssistanceDetails { get; private set; }

    public PetPhotoList PetPhotos { get; private set; }

    public static Result<Pet> Create(PetId id,
        string nickname,
        string description,
        string breed,
        string color,
        string informationHealth,
        Address address,
        double weight,
        double height,
        string contactPhoneNumber,
        bool isNeutered,
        DateOnly dateOfBirth,
        bool isVaccinated,
        HelpStatuses helpStatus)
    {
        if (string.IsNullOrWhiteSpace(nickname))
            return Result.Failure<Pet>("Nickname cannot be empty");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Pet>("Description cannot be empty");
        
        if (string.IsNullOrWhiteSpace(breed))
            return Result.Failure<Pet>("Breed cannot be empty");
        
        if (string.IsNullOrWhiteSpace(color))
            return Result.Failure<Pet>("Color cannot be empty");
        
        if (string.IsNullOrWhiteSpace(informationHealth))
            return Result.Failure<Pet>("Information health cannot be empty");
        
        if (string.IsNullOrWhiteSpace(contactPhoneNumber))
            return Result.Failure<Pet>("Phone number cannot be empty");

        return new Pet(
            id,
            nickname,
            description,
            breed,
            color,
            informationHealth,
            address,
            weight,
            height,
            contactPhoneNumber,
            isNeutered,
            dateOfBirth,
            isVaccinated,
            helpStatus);
    }
}