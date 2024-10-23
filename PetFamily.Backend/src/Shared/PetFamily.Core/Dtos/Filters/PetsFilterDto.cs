namespace PetFamily.Core.Dtos.Filters;

public record PetsFilterDto(
    Guid? Id,
    string? Name,
    string? Description,
    Guid? SpeciesId,
    Guid? BreedId,
    string? Color,
    string? InformationHealth,
    double? WeightFrom,
    double? WeightTo,
    double? HeightFrom,
    double? HeightTo,
    string? PhoneNumber,
    bool? IsNeutered,
    DateOnly? DateOfBirthFrom,
    DateOnly? DateOfBirthTo,
    bool? IsVaccinated,
    string? HelpStatus,
    DateTime? CreatedAtFrom,
    DateTime? CreatedAtTo,
    int? PositionFrom,
    int? PositionTo);