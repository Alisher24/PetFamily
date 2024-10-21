using PetFamily.Core.Dtos;
using PetFamily.Volunteers.Application.Volunteers.Commands.UpdateSocialNetworks;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record UpdateSocialNetworksRequest(
    Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateSocialNetworksCommand ToCommand() =>
        new(VolunteerId, SocialNetworks);
}