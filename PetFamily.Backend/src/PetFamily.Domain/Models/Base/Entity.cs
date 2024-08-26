namespace Domain.Models.Base;

public abstract class Entity<TId>(TId id)
    where TId : notnull
{
    public TId Id { get; private set; } = id;
}