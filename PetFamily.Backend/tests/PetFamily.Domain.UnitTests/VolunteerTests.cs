using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Aggregates.Volunteer;
using Domain.Aggregates.Volunteer.Entities;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Enums;
using FluentAssertions;
using Type = Domain.Aggregates.Species.ValueObjects.Type;

namespace PetFamily.UnitTests;

public class VolunteerTests
{
    [Fact]
    public void AddPet_With_Empty_Pets_Approach_Return_Success_Result()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pet = GeneratePet();

        // act
        var result = volunteer.AddPet(pet);

        // assert
        var addedPetResult = volunteer.GetPetById(pet.Id);

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(pet.Id);
        addedPetResult.Value.SerialNumber.Value.Should().Be(1);
    }

    [Fact]
    public void AddPet_With_Other_Pets_Approach_Return_Success_Result()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet());

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var petToAdd = GeneratePet();

        // act
        var result = volunteer.AddPet(petToAdd);

        // assert
        var addedPetResult = volunteer.GetPetById(petToAdd.Id);

        result.IsSuccess.Should().BeTrue();
        addedPetResult.IsSuccess.Should().BeTrue();
        addedPetResult.Value.Id.Should().Be(petToAdd.Id);
        addedPetResult.Value.SerialNumber.Value.Should().Be(volunteer.Pets.Count);
    }
    
    [Fact]
    public void MovePet_To_Any_Position_Approach_Return_Success_Result()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 10).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        // act
        var result = volunteer.MovePet(pets[4], 7);
        
        // assert
        var movedPetResult = volunteer.Pets
            .Where(p => p.SerialNumber.Value == 7);

        movedPetResult.Count().Should().Be(1);
        movedPetResult.First().Id.Should().Be(pets[4].Id);
    }

    private Volunteer GenerateVolunteer()
    {
        var fullName = FullName.Create("test", "test", "test").Value;
        var email = Email.Create("test@mail.ru").Value;
        var description = Description.Create("test").Value;
        var yearsExperience = YearsExperience.Create(1).Value;
        var phoneNumber = PhoneNumber.Create("+79111111111").Value;
        var volunteer = new Volunteer(
            VolunteerId.NewVolunteerId(),
            fullName,
            email,
            description,
            yearsExperience,
            phoneNumber,
            new ValueObjectList<SocialNetwork>([]),
            new ValueObjectList<Requisite>([]));

        return volunteer;
    }

    private Pet GeneratePet()
    {
        var petId = PetId.NewPetId();
        var name = Name.Create("test").Value;
        var description = Description.Create("test").Value;
        var type = new Type(SpeciesId.NewSpeciesId(), Guid.NewGuid());
        var color = Color.Create("test").Value;
        var informationHealth = InformationHealth.Create("test").Value;
        var address = Address.Create("test", "test", "test", "test").Value;
        var weight = Weight.Create(1).Value;
        var height = Height.Create(1).Value;
        var phoneNumber = PhoneNumber.Create("+79111111111").Value;
        var dateOfBirth = DateOfBirth.Create(DateOnly.FromDateTime(DateTime.UtcNow)).Value;
        var pet = new Pet(
            petId,
            name,
            description,
            type,
            color,
            informationHealth,
            address,
            weight,
            height,
            phoneNumber,
            false,
            dateOfBirth,
            false,
            HelpStatuses.NeedsHelp,
            new ValueObjectList<Requisite>([]));

        return pet;
    }
}