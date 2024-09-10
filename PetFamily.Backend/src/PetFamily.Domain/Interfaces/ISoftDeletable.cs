namespace Domain.Interfaces;

public interface ISoftDeletable
{
    void Delete();

    void Restore();
}