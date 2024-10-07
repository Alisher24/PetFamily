using Application.Abstraction;

namespace Application.SpeciesManagement.Breeds.Delete;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;