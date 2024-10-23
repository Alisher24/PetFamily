namespace PetFamily.SharedKernel.Interfaces;

public interface ISoftDeletable
{
    void Deactivate();

    void Restore();
}