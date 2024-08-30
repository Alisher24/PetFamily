using CSharpFunctionalExtensions;
using Domain.Models.CommonFields;

namespace Domain.Models.ValueObjects;

public record AssistanceDetail
{
    private AssistanceDetail() { }
    
    private AssistanceDetail(Name name, Description description)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; } = default!;
    public Description Description { get; } = default!;

    public static Result<AssistanceDetail> Create(Name name, Description description)
    {
        return new AssistanceDetail(name, description);
    }
}