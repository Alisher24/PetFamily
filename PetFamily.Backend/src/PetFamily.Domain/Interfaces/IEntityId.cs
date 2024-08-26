namespace Domain.Interfaces;

public interface IEntityId<T>
{
    public T Id { get; set; }
}