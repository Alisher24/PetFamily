namespace Domain.Aggregates.Volunteer.ValueObjects;

public record RequisiteList
{
    private RequisiteList()
    {
    }

    public RequisiteList(IEnumerable<Requisite> requisites)
    {
        Requisites = requisites.ToList();
    }

    public IReadOnlyList<Requisite> Requisites { get; }
}