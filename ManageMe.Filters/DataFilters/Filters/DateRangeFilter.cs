using ManageMe.Core;

namespace ManageMe.Filters;

public class DateRangeFilter<T> : IDataFilter<T> where T : IHasDate
{
    private readonly DateTime _start;

    private readonly DateTime _end;

    public int Priority => 0;

    public DateRangeFilter(DateTime start, DateTime end)
    {
        _start = start;
        _end = end;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.Where(x => x.CreatedAt >= _start && x.CreatedAt < _end);
    }
}
