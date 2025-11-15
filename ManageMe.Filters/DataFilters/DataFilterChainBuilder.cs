namespace ManageMe.Filters;

public abstract class DataFilterChainBuilder<T>
{
    private readonly Dictionary<string, Func<DataFilterTokenCollection, IDataFilter<T>?>> _factoryMap;
    
    private readonly DataFilterTokenCollection _collection;

    public DataFilterTokenCollection Collection { get => _collection; }

    public DataFilterChainBuilder(DataFilterTokenCollection collection)
    {
        _factoryMap = BuildFactories();
        _collection = collection;
    }

    protected abstract Dictionary<string, Func<DataFilterTokenCollection, IDataFilter<T>?>> BuildFactories();

    public abstract DataFilterChain<T> Build();

    public IDataFilter<T>? GetDataFilter(string token)
    {
        if(!_factoryMap.TryGetValue(token, out var filter))
        {
            return null;
        }

        return filter(_collection);
    }
}
