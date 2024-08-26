using Domain.Interfaces;

namespace Domain.Models;

public class SocialNetwork : IEntityId<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Link { get; set; }
    public required Volunteer Volunteer { get; set; }
    public Guid VolunteerId { get; set; }
}