using Application.Database;
using Application.Dtos;
using Application.SpeciesManagement;
using Application.VolunteerManagement;
using Application.VolunteerManagement.Pets.AddPet;
using Domain.Aggregates.Species;
using Domain.Aggregates.Species.Entities;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Aggregates.Volunteer;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Enums;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;

namespace PetFamily.Application.UnitTests;

public class AddPetServiceTests
{
    private readonly Mock<IVolunteerRepository> _volunteerRepositoryMock = new();
    private readonly Mock<ISpeciesRepository> _speciesRepositoryMock = new();
    private readonly Mock<IValidator<AddPetCommand>> _validatorMock = new();
    private readonly Mock<ILogger<AddPetService>> _loggerMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();


    [Fact]
    public async void AddPetService_Should_Add_Pet_To_Volunteer()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var volunteer = GenerateVolunteer();
        var species = GenerateSpecies();
        var breed = GenerateBreed(species);
        var command = GenerateCommand(volunteer.Id, species.Id, breed.Id);

        _volunteerRepositoryMock
            .Setup(v => v.GetByIdAsync(command.VolunteerId, cancellationToken))
            .ReturnsAsync(volunteer);

        _speciesRepositoryMock
            .Setup(s => s.GetByIdAsync(command.SpeciesId, cancellationToken))
            .ReturnsAsync(species);

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        var service = new AddPetService(
            _volunteerRepositoryMock.Object,
            _speciesRepositoryMock.Object,
            _validatorMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);

        // act
        var result = await service.ExecuteAsync(command, cancellationToken);
        var petResult = volunteer.GetPetById(result.Value);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        volunteer.Pets.Count.Should().NotBe(0);
        petResult.IsSuccess.Should().BeTrue();
        petResult.IsFailure.Should().BeFalse();
        petResult.Value.Id.Value.Should().Be(result.Value);
        petResult.Value.Type.SpeciesId.Should().Be(species.Id);
        petResult.Value.Type.BreedId.Should().Be(breed.Id);
    }

    [Fact]
    public async void AddPetService_Should_Return_Failure_When_Validation_Is_Fail()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var volunteer = GenerateVolunteer();
        var species = GenerateSpecies();
        var breed = GenerateBreed(species);
        var validationResult = new ValidationResult([
            new ValidationFailure("test", "test||test||Validation")
            {
                ErrorCode = "test"
            }
        ]);
        var command = GenerateCommand(volunteer.Id, species.Id, breed.Id);

        _volunteerRepositoryMock
            .Setup(v => v.GetByIdAsync(command.VolunteerId, cancellationToken))
            .ReturnsAsync(volunteer);

        _speciesRepositoryMock
            .Setup(s => s.GetByIdAsync(command.SpeciesId, cancellationToken))
            .ReturnsAsync(species);

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(validationResult);

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        var service = new AddPetService(
            _volunteerRepositoryMock.Object,
            _speciesRepositoryMock.Object,
            _validatorMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);

        // act
        var result = await service.ExecuteAsync(command, cancellationToken);

        // assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        volunteer.Pets.Count.Should().Be(0);
    }

    private AddPetCommand GenerateCommand(Guid volunteerId, Guid speciesId, Guid breedId)
    {
        var name = "Test";
        var description = "Test";
        var color = "Test";
        var informationHealth = "Test";
        var address = new AddressDto("Test", "Test", "Test", "Test");
        var weight = 1d;
        var height = 2d;
        var phoneNumber = "+79333333333";
        var isNeutered = false;
        var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow);
        var isVaccinated = true;
        var helpStatuses = HelpStatuses.FoundHouse;
        IEnumerable<RequisiteDto> requisites = [];

        return new(volunteerId,
            name,
            description,
            speciesId,
            breedId,
            color,
            informationHealth,
            address,
            weight,
            height,
            phoneNumber,
            isNeutered,
            dateOfBirth,
            isVaccinated,
            helpStatuses,
            requisites);
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

    private Species GenerateSpecies()
    {
        var id = SpeciesId.NewSpeciesId();
        var name = Name.Create("Test").Value;
        var description = Description.Create("test").Value;

        return new Species(id, name, description);
    }

    private Breed GenerateBreed(Species species)
    {
        var id = BreedId.NewBreedId();
        var name = Name.Create("Test").Value;
        var description = Description.Create("test").Value;
        var breed = new Breed(id, name, description);

        species.AddBread(breed);

        return breed;
    }
}