namespace ManageMe.Filters;

public class PageDataFilter<T> : IDataFilter<T>
{
    private readonly int _page;

    private readonly int _size;

    public int Priority => -1;

    public int Page { get { return _page; } }

    public int Size { get { return _size; } }

    public PageDataFilter(int page = 1, int size = 20)
    {
        _page = page;
        _size = size;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.Skip((_page - 1) * _size).Take(_size);
    }
}
