using Application.Database;
using Application.Dtos;
using Application.FileProvider;
using Application.VolunteerManagement;
using Application.VolunteerManagement.Pets.AddPetPhotos;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Aggregates.Volunteer;
using Domain.Aggregates.Volunteer.Entities;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Shared;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using Type = Domain.Aggregates.Species.ValueObjects.Type;

namespace PetFamily.Application.UnitTests;

public class AddPetPhotosTests
{
    private readonly Mock<IFileProvider> _fileProviderMock = new();
    private readonly Mock<IVolunteerRepository> _volunteerRepositoryMock = new();
    private readonly Mock<IValidator<AddPetPhotosCommand>> _validatorMock = new();
    private readonly Mock<ILogger<AddPetPhotosService>> _loggerMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [Fact]
    public async void AddPetPhotosService_Should_Add_One_Photo_To_Pet()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var volunteer = GenerateVolunteer();
        var pet = GeneratePet();
        volunteer.AddPet(pet);

        var stream = new MemoryStream();
        var fileName = "test.jpg";

        var command = new AddPetPhotosCommand(volunteer.Id, pet.Id, [new UploadFileDto(stream, fileName)]);

        var filePath = PhotoPath.Create(Guid.NewGuid(), ".jpg").Value;

        _fileProviderMock.Setup(f => f.UploadFileAsync(It.IsAny<List<FileData>>(), cancellationToken))
            .ReturnsAsync(Result<IReadOnlyList<IFilePath>>.Success([filePath]));

        _volunteerRepositoryMock.Setup(v => v.GetByIdAsync(volunteer.Id, cancellationToken))
            .ReturnsAsync(volunteer);

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        var service = new AddPetPhotosService(
            _fileProviderMock.Object,
            _volunteerRepositoryMock.Object,
            _validatorMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);

        // act
        var result = await service.ExecuteAsync(command, cancellationToken);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        pet.PetPhotos.Values.Count.Should().Be(1);
        pet.PetPhotos.Values.First().Path.Should().Be(filePath);
    }

    [Fact]
    public async void AddPetPhotosService_Should_Add_Some_Photos_To_Pet()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var volunteer = GenerateVolunteer();
        var pet = GeneratePet();
        volunteer.AddPet(pet);

        var stream = new MemoryStream();
        var fileName = "test.jpg";

        var command = new AddPetPhotosCommand(volunteer.Id, pet.Id,
        [
            new UploadFileDto(stream, fileName),
            new UploadFileDto(stream, fileName),
            new UploadFileDto(stream, fileName)
        ]);

        PhotoPath[] filePaths =
        [
            PhotoPath.Create(Guid.NewGuid(), ".jpg").Value,
            PhotoPath.Create(Guid.NewGuid(), ".jpg").Value,
            PhotoPath.Create(Guid.NewGuid(), ".jpg").Value
        ];

        _fileProviderMock.Setup(f => f.UploadFileAsync(It.IsAny<List<FileData>>(), cancellationToken))
            .ReturnsAsync(Result<IReadOnlyList<IFilePath>>.Success(filePaths));

        _volunteerRepositoryMock.Setup(v => v.GetByIdAsync(volunteer.Id, cancellationToken))
            .ReturnsAsync(volunteer);

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        var service = new AddPetPhotosService(
            _fileProviderMock.Object,
            _volunteerRepositoryMock.Object,
            _validatorMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);

        // act
        var result = await service.ExecuteAsync(command, cancellationToken);
        var firstPhoto = filePaths[0];
        var secondPhoto = filePaths[1];
        var thirdPhoto = filePaths[2];

        // assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        pet.PetPhotos.Values.Count.Should().Be(filePaths.Length);
        pet.PetPhotos.Values[0].Path.Should().Be(firstPhoto);
        pet.PetPhotos.Values[1].Path.Should().Be(secondPhoto);
        pet.PetPhotos.Values[2].Path.Should().Be(thirdPhoto);
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
        var name = Name.Create("Test").Value;
        var description = Description.Create("Test").Value;
        var type = new Type(SpeciesId.NewSpeciesId(), Guid.NewGuid());
        var color = Color.Create("Test").Value;
        var informationHealth = InformationHealth.Create("Test").Value;
        var address = Address.Create("Test", "Test", "Test", "Test").Value;
        var weight = Weight.Create(1d).Value;
        var height = Height.Create(2d).Value;
        var phoneNumber = PhoneNumber.Create("+79333333333").Value;
        var isNeutered = false;
        var dateOfBirth = DateOfBirth.Create(DateOnly.FromDateTime(DateTime.UtcNow)).Value;
        var isVaccinated = true;
        var helpStatuses = HelpStatuses.FoundHouse;
        var requisites = new ValueObjectList<Requisite>([]);

        return new Pet(petId,
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
            helpStatuses,
            requisites);
    }
}