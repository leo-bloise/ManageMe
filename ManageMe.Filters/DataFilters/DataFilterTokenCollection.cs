using System.Collections.Immutable;

namespace ManageMe.Filters;

public class DataFilterTokenCollection
{
    private readonly Dictionary<string, string> _parameters;

    public DataFilterTokenCollection(Dictionary<string, string> parameters)
    {
        _parameters = parameters;
    }

    public object? GetTokenValue(string token)
    {
        _parameters.TryGetValue(token, out var value);

        return value;
    }

    public ImmutableArray<string> Tokens { get => _parameters.Keys.ToImmutableArray();  }
}
