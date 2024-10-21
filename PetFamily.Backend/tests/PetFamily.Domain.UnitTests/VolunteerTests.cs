using System.Drawing;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Aggregates.Volunteer;
using Domain.Aggregates.Volunteer.Entities;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Enums;
using FluentAssertions;
using Type = Domain.Aggregates.Species.ValueObjects.Type;

namespace PetFamily.Domain.UnitTests;

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
        addedPetResult.Value.Position.Value.Should().Be(1);
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
        addedPetResult.Value.Position.Value.Should().Be(volunteer.Pets.Count);
    }

    [Fact]
    public void MovePet_Should_Not_Move_When_Pet_Already_At_New_Position()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 10).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        // act
        var result = volunteer.MovePet(pets[4], 5);

        // assert
        var movedPetResult = volunteer.Pets
            .Where(p => p.Position == 5);

        result.IsSuccess.Should().BeTrue();
        movedPetResult.Count().Should().Be(1);
        movedPetResult.First().Id.Should().Be(pets[4].Id);
    }

    [Fact]
    public void MovePet_Should_Move_Pet_To_Forward()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var firstPet = pets[0];
        var secondPet = pets[1];
        var thirdPet = pets[2];
        var fourthPet = pets[3];
        var fifthPet = pets[4];

        // act
        var result = volunteer.MovePet(secondPet, 4);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(4);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void MovePet_Should_Move_Pet_To_Back()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var firstPet = pets[0];
        var secondPet = pets[1];
        var thirdPet = pets[2];
        var fourthPet = pets[3];
        var fifthPet = pets[4];

        // act
        var result = volunteer.MovePet(fourthPet, 2);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(2);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void MovePet_Should_Move_Pet_To_First()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var firstPet = pets[0];
        var secondPet = pets[1];
        var thirdPet = pets[2];
        var fourthPet = pets[3];
        var fifthPet = pets[4];

        // act
        var result = volunteer.MovePet(fourthPet, 1);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(2);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(1);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void MovePet_Should_Move_Pet_To_Last()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var firstPet = pets[0];
        var secondPet = pets[1];
        var thirdPet = pets[2];
        var fourthPet = pets[3];
        var fifthPet = pets[4];

        // act
        var result = volunteer.MovePet(secondPet, 5);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(5);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(4);
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
            new List<SocialNetwork>([]),
            new List<Requisite>([]));

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
            new List<Requisite>([]));

        return pet;
    }
}