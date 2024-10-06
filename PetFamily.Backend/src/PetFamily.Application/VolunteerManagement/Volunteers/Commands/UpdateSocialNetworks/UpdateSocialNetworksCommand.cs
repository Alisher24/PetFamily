using Application.Abstraction;
using Application.Dtos;

namespace Application.VolunteerManagement.Volunteers.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;