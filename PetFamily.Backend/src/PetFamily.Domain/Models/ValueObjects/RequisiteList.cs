namespace Domain.Models.ValueObjects;

public record RequisiteList
{
    public IReadOnlyList<Requisite> AssistanceDetails { get; } = [];
}