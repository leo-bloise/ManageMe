using ManageMe.Core.Exceptions;
using ManageMe.Filters;

namespace ManageMe.Core.Filters;

public class DataFilterChainTransactionBuilder : DataFilterChainBuilder<Transaction>
{
    public DataFilterChainTransactionBuilder(DataFilterTokenCollection collection) : base(collection)
    {
    }

    public override DataFilterChain<Transaction> Build()
    {
        DataFilterChain<Transaction> dataFilterChain = new DataFilterChain<Transaction>();

        foreach(string token in Collection.Tokens)
        {
            IDataFilter<Transaction>? filter = GetDataFilter(token);

            if (filter is null)
            {
                continue;
            }

            dataFilterChain.Add(filter);
        }

        return dataFilterChain;
    }

    private IDataFilter<Transaction>? BuildPageAndSize(DataFilterTokenCollection collection)
    {
        string? page = collection.GetTokenValue("page") as string;
        string? size = collection.GetTokenValue("size") as string;

        if (string.IsNullOrEmpty(page) || string.IsNullOrEmpty(size))
        {
            return null;
        }

        if (!int.TryParse(page, out int pageValue))
        {
            throw new InvalidFilterValue("page", "page must be an int");
        }

        if (pageValue <= 0)
        {
            throw new InvalidFilterValue("page", "page must be bigger than 0");
        }

        if (!int.TryParse(size, out int sizeValue))
        {
            throw new InvalidFilterValue("size", "size must be an int");
        }

        if (sizeValue <= 0)
        {
            throw new InvalidFilterValue("size", "size must be bigger than 0");
        }

        return new PageDataFilter<Transaction>(pageValue, sizeValue);
    }

    private IDataFilter<Transaction>? BuildDateFilter(DataFilterTokenCollection collection)
    {
        string? start = collection.GetTokenValue("start") as string;
        string? end = collection.GetTokenValue("end") as string;

        if (string.IsNullOrEmpty(start)) return null;

        if(!DateTime.TryParse(start, out DateTime startValue))
        {
            throw new InvalidFilterValue("start", "start parameter must have a valid date in ISO format");
        }

        DateTime endValue = startValue.AddDays(1);

        if (!string.IsNullOrEmpty(end) && !DateTime.TryParse(end, out endValue))
        {
            throw new InvalidFilterValue("end", "end parameter must have a valid date in ISO format");
        }

        return new DateRangeFilter<Transaction>(startValue, endValue);
    }

    protected override Dictionary<string, Func<DataFilterTokenCollection, IDataFilter<Transaction>?>> BuildFactories()
    {
        return new Dictionary<string, Func<DataFilterTokenCollection, IDataFilter<Transaction>?>>()
        {
            { "page", BuildPageAndSize },
            { "start", BuildDateFilter }
        };
    }
}
