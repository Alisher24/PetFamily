using Application.Dtos;

namespace Application.VolunteerManagement.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId,
    UpdateMainInfoDto Dto);