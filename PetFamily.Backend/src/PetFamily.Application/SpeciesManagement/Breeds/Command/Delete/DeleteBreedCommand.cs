using Application.Abstraction;

namespace Application.SpeciesManagement.Breeds.Command.Delete;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;