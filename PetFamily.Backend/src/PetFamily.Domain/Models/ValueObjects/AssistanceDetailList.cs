namespace Domain.Models.ValueObjects;

public record AssistanceDetailList
{
    public IReadOnlyList<AssistanceDetail> AssistanceDetails { get; } = [];
}