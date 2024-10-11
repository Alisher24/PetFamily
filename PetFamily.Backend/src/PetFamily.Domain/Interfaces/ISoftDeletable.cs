namespace Domain.Interfaces;

public interface ISoftDeletable
{
    void Deactivate();

    void Restore();
}