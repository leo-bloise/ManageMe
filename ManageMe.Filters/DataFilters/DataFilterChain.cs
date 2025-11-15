using System.Collections;

namespace ManageMe.Filters;

public class DataFilterChain<T> : IEnumerable<IDataFilter<T>>
{
    private readonly IList<IDataFilter<T>> _filters = new List<IDataFilter<T>>();

    public DataFilterChain<T> Add(IDataFilter<T> filter)
    {
        _filters.Add(filter);
        return this;
    }

    public IEnumerator<IDataFilter<T>> GetEnumerator()
    {
        return _filters.OrderByDescending(filter => filter.Priority).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
