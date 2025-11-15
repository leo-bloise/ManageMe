namespace ManageMe.Filters;

public interface IDataFilter<T>
{
    public int Priority { get; }
    IQueryable<T> Apply(IQueryable<T> query);
}