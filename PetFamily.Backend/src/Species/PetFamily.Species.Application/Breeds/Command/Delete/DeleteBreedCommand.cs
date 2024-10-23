using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Breeds.Command.Delete;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;