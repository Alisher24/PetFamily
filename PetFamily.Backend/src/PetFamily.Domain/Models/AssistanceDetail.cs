using Domain.Interfaces;

namespace Domain.Models;

public class AssistanceDetail : IEntityId<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required Pet Pet { get; set; }
    public Guid PetId { get; set; }
}