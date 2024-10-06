using Application.Abstraction;
using Application.Dtos;

namespace Application.VolunteerManagement.Volunteers.Commands.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId,
    UpdateMainInfoDto Dto) : ICommand;