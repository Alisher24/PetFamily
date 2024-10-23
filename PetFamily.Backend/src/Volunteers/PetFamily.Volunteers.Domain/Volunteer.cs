using PetFamily.SharedKernel.Interfaces;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.Volunteers.Domain.Entities;
using PetFamily.Volunteers.Domain.ValueObjects;
using Type = PetFamily.Volunteers.Domain.ValueObjects.Type;

namespace PetFamily.Volunteers.Domain;

public sealed class Volunteer : Entity<VolunteerId>, ISoftDeletable
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

    public Result AddPetPhotos(Guid petId, List<PetPhoto> petPhotos)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        petResult.Value.AddPhotos(petPhotos);

        return Result.Success();
    }

    public Result<List<PhotoPath>> DeletePetPhotos(Guid petId, List<string> photoPaths)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        var petPhotos = new List<PetPhoto>();
        var paths = new List<PhotoPath>();
        foreach (var photoPath in photoPaths)
        {
            var petPhoto = petResult.Value.PetPhotos
                .FirstOrDefault(p => p.Path.Value == photoPath);
            if (petPhoto is null)
                return Errors.General
                    .NotFound($"Photo with path: {photoPath} of the pet with id: {petId}");

            petPhotos.Add(petPhoto);
            paths.Add(petPhoto.Path);
        }

        petResult.Value.DeletePhotos(petPhotos);

        return paths;
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

    public Result UpdatePet(
        Guid petId,
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
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        petResult.Value.Update(
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
            helpStatus,
            requisites);

        return Result.Success();
    }

    public Result UpdatePetStatus(Guid petId, HelpStatuses helpStatuses)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        petResult.Value.UpdateStatus(helpStatuses);

        return Result.Success();
    }

    public Result AssignMainPetPhoto(Guid petId, PhotoPath photoPath)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        var assignMainPetPhotoResult = petResult.Value.AssignMainPhoto(photoPath);
        if (assignMainPetPhotoResult.IsFailure)
            return assignMainPetPhotoResult.ErrorList;

        return Result.Success();
    }

    public Result<Pet> DeletePet(Guid petId)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.ErrorList;

        _pets.Remove(petResult.Value);

        return petResult;
    }

    public Result<Pet> GetPetById(Guid petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id.Value == petId);
        if (pet is null)
            return Errors.General.NotFound($"Pet with id: {petId}");

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