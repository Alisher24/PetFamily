using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId,
    UpdateMainInfoDto Dto) : ICommand;