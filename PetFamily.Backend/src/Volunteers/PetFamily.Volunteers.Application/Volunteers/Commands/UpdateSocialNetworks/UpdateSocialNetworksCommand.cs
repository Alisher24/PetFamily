using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworksCommand(Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;