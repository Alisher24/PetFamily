using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
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

    public Pet(PetId id,
        Name name,
        Description description,
        Type type,
        Color color,
        InformationHealth informationHealth,
        Address address,
        Weight weight,
        Height height,
        PhoneNumber phoneNumber,
        bool isNeutered,
        DateOfBirth dateOfBirth,
        bool isVaccinated,
        HelpStatuses helpStatus,
        ValueObjectList<Requisite> requisites) : base(id)
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
        CreatedAt = DateTime.UtcNow;
        Requisites = requisites;
        PetPhotos = new ValueObjectList<PetPhoto>([]);
    }

    public Name Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public Type Type { get; private set; } = default!;

    public Color Color { get; private set; } = default!;

    public InformationHealth InformationHealth { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public Weight Weight { get; private set; } = default!;

    public Height Height { get; private set; } = default!;

    public PhoneNumber PhoneNumber { get; private set; } = default!;

    public bool IsNeutered { get; private set; }

    public DateOfBirth DateOfBirth { get; private set; } = default!;

    public bool IsVaccinated { get; private set; }

    public HelpStatuses HelpStatus { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Position Position { get; private set; } = default!;

    public ValueObjectList<Requisite> Requisites { get; private set; } = default!;

    public ValueObjectList<PetPhoto> PetPhotos { get; private set; } = default!;

    public void AddPhotos(List<PetPhoto> petPhotos)
    {
        var photos = PetPhotos.Values.ToList();
        photos.AddRange(petPhotos);
        PetPhotos = photos;
    }

    public void SetPosition(Position position) =>
        Position = position;

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
}