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
    private readonly List<PetPhoto> _petPhotos = [];

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
        IReadOnlyList<Requisite> requisites) : base(id)
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

    public IReadOnlyList<Requisite> Requisites { get; private set; } = default!;

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;

    internal void AddPhotos(List<PetPhoto> petPhotos) => _petPhotos.AddRange(petPhotos);

    internal void DeletePhotos(List<PetPhoto> petPhotos)
    {
        petPhotos.ForEach(p => _petPhotos.Remove(p));
    }

    internal void SetPosition(Position position) =>
        Position = position;

    internal void Update(
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
        IReadOnlyList<Requisite> requisites)
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
        Requisites = requisites;
    }

    internal void UpdateStatus(HelpStatuses helpStatuses) => HelpStatus = helpStatuses;

    internal Result AssignMainPhoto(PhotoPath photoPath)
    {
        var check = false;
        var photoIndex = 0;
        for (var i = 0; i < _petPhotos.Count; i++)
        {
            if (_petPhotos[i].Path == photoPath)
            {
                photoIndex = i;
                check = true;
                break;
            }
        }

        if (check == false)
            return Errors.General.NotFound(
                $"Photo with path: {photoPath.Value} for animal with id: {Id.Value}");

        if (_petPhotos[photoIndex].IsMain)
            return Result.Success();

        if (_petPhotos.Count == 1)
        {
            _petPhotos[0] = new PetPhoto(_petPhotos.First().Path, true);
            return Result.Success();
        }

        _petPhotos[photoIndex] = new PetPhoto(_petPhotos.First().Path, false);
        _petPhotos[0] = new PetPhoto(photoPath, true);

        return Result.Success();
    }

    public void Deactivate()
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