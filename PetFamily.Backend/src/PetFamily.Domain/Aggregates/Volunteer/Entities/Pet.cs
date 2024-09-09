using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonFields;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;
using Type = Domain.Aggregates.Species.ValueObjects.Type;

namespace Domain.Aggregates.Volunteer.Entities;

public class Pet : Entity<PetId>, ISoftDeletable
{
    private bool _isDeleted = false;
    
    // ef core
    private Pet(PetId id) : base(id)
    {
    }

    private Pet(PetId id,
        Name name,
        Description description,
        Type type,
        string color,
        string informationHealth,
        Address address,
        double weight,
        double height,
        PhoneNumber phoneNumber,
        bool isNeutered,
        DateOnly dateOfBirth,
        bool isVaccinated,
        HelpStatuses helpStatus) : base(id)
    {
        Name = name;
        Description = description;
        Type = type;
        Color = color;
        InformationHealth = informationHealth;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        IsNeutered = isNeutered;
        DateOfBirth = dateOfBirth;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
        CreatedAt = DateTime.Now;
    }

    public Name Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public Type Type { get; private set; } = default!;

    public string Color { get; private set; } = default!;

    public string InformationHealth { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public double Weight { get; private set; }

    public double Height { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; } = default!;

    public bool IsNeutered { get; private set; }

    public DateOnly DateOfBirth { get; private set; }

    public bool IsVaccinated { get; private set; }

    public HelpStatuses HelpStatus { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public RequisiteList Requisites { get; private set; }

    public PetPhotoList PetPhotos { get; private set; }
    
    public void Delete()
    {
        if (!_isDeleted)
            _isDeleted = true;
    }
    
    public void Restore()
    {
        if (_isDeleted)
            _isDeleted = false;
    }

    public static Result<Pet> Create(PetId id,
        Name name,
        Description description,
        Type type,
        string color,
        string informationHealth,
        Address address,
        double weight,
        double height,
        PhoneNumber phoneNumber,
        bool isNeutered,
        DateOnly dateOfBirth,
        bool isVaccinated,
        HelpStatuses helpStatus)
    {
        if (string.IsNullOrWhiteSpace(color))
            return Errors.General.ValueIsInvalid("Color");

        if (string.IsNullOrWhiteSpace(informationHealth))
            return Errors.General.ValueIsInvalid("Information health");

        return new Pet(
            id,
            name,
            description,
            type,
            color,
            informationHealth,
            address,
            weight,
            height,
            phoneNumber,
            isNeutered,
            dateOfBirth,
            isVaccinated,
            helpStatus);
    }
}