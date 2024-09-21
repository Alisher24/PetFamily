using Application.Dtos;

namespace Application.VolunteerManagement.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetworks);