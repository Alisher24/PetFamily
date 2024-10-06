using Domain.Aggregates.Volunteer.Entities;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer;

public sealed class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    private readonly List<Pet> _pets = [];

    private bool _isDeleted = false;

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
        IReadOnlyList<SocialNetwork> socialNetworks,
        IReadOnlyList<Requisite> requisites) : base(id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsExperience = yearsExperience;
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworks;
        Requisites = requisites;
    }

    public FullName FullName { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public YearsExperience YearsExperience { get; private set; } = default!;

    public PhoneNumber PhoneNumber { get; private set; } = default!;

    public IReadOnlyList<SocialNetwork> SocialNetworks { get; private set; }

    public IReadOnlyList<Requisite> Requisites { get; private set; }

    public IReadOnlyList<Pet> Pets => _pets;

    public int PetsAdopted => _pets
        .Count(x => x.HelpStatus == HelpStatuses.FoundHouse);

    public int CurrentPetsSeekingHome => _pets
        .Count(x => x.HelpStatus == HelpStatuses.LookingForHome);

    public int PetsUnderTreatment => _pets
        .Count(x => x.HelpStatus == HelpStatuses.NeedsHelp);

    public Result AddPet(Pet pet)
    {
        var serialNumber = Position
            .Create(_pets.Count + 1);
        if (serialNumber.IsFailure)
            return serialNumber.ErrorList;

        pet.SetPosition(serialNumber.Value);
        _pets.Add(pet);

        return Result.Success();
    }

    public Result MovePet(Pet pet, int newPosition)
    {
        if (newPosition > _pets.Count)
            return Errors.General.ValueIsInvalid("position");

        if (pet.Position.Value == newPosition)
            return Result.Success();

        var positionResult = Position.Create(newPosition);
        if (positionResult.IsFailure)
            return positionResult.ErrorList;

        var moveResult = MovePetsBetweenPositions(pet.Position, in newPosition);
        if (moveResult.IsFailure)
            return moveResult.ErrorList;

        pet.SetPosition(positionResult.Value);
        return Result.Success();
    }

    private Result MovePetsBetweenPositions(Position currentPosition, in int newPosition)
    {
        if (newPosition > currentPosition)
        {
            foreach (var value in _pets)
            {
                if (value.Position > currentPosition
                    && newPosition >= value.Position)
                {
                    var positionResult = Position.Create(value.Position - 1);
                    if (positionResult.IsFailure)
                        return positionResult.ErrorList;
                    value.SetPosition(positionResult.Value);
                }
            }
        }
        else
        {
            foreach (var value in _pets)
            {
                if (value.Position < currentPosition
                    && newPosition <= value.Position)
                {
                    var positionResult = Position.Create(value.Position + 1);
                    if (positionResult.IsFailure)
                        return positionResult.ErrorList;
                    value.SetPosition(positionResult.Value);
                }
            }
        }

        return Result.Success();
    }

    public Result<Pet> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            return Errors.General.NotFound($"Pet with id: {petId.Value}");

        return pet;
    }

    public void UpdateMainInfo(
        FullName fullName,
        Email email,
        Description description,
        YearsExperience yearsExperience,
        PhoneNumber phoneNumber)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        YearsExperience = yearsExperience;
        PhoneNumber = phoneNumber;
    }

    public void UpdateSocialNetworks(IReadOnlyList<SocialNetwork> socialNetworks) =>
        SocialNetworks = socialNetworks;

    public void UpdateRequisites(IReadOnlyList<Requisite> requisites) =>
        Requisites = requisites;

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