using CSharpFunctionalExtensions;

namespace Domain.Models.Shared;

public record AssistanceDetail
{
    private AssistanceDetail(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; } = default!;
    public string Description { get; } = default!;

    public static Result<AssistanceDetail> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<AssistanceDetail>("Name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<AssistanceDetail>("Description cannot be empty");

        return new AssistanceDetail(name, description);
    }
}