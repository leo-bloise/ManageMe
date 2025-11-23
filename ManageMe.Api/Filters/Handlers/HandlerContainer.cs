using ManageMe.Application.Exceptions;

namespace ManageMe.Api.Filters.Handlers;

public class HandlerContainer(ILogger<HandlerContainer> logger, GeneralExceptionHandler generalHandler)
{
    public Dictionary<Type, IExceptionHandler> _values = new (); 

    public void Register<T>(IExceptionHandler handler)
    {
        if (_values.ContainsKey(typeof(T))) throw new Exception($"Handler for {typeof(T).Name} was already registered");
        _values[typeof(T)] = handler;

        logger.LogInformation($"Handler for {typeof(T).Name} registered");
    }

    public IExceptionHandler ForException(Type metadata)
    {
        if (!typeof(Exception).IsAssignableFrom(metadata))
            throw new Exception($"Type {metadata.Name} must be an Exception");

        logger.LogDebug($"Handler for {metadata.Name} retrieved");

        IExceptionHandler? bestMatch = null;
        int bestDistance = int.MaxValue;

        foreach (var kv in _values)
        {
            var registeredType = kv.Key;

            if (registeredType.IsAssignableFrom(metadata))
            {
                int distance = GetInheritanceDistance(metadata, registeredType);

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestMatch = kv.Value;
                }
            }
        }

        return bestMatch ?? generalHandler;
    }

    private static int GetInheritanceDistance(Type derived, Type baseType)
    {
        int distance = 0;
        var current = derived;

        while (current != null)
        {
            if (current == baseType)
                return distance;

            distance++;
            current = current.BaseType;
        }

        return int.MaxValue;
    }


    public static HandlerContainer WithDefaults(IServiceProvider provider)
    {
        HandlerContainer container = new HandlerContainer(provider.GetRequiredService<ILogger<HandlerContainer>>(), provider.GetRequiredService<GeneralExceptionHandler>());

        container.Register<AppException>(provider.GetRequiredService<ApplicationExceptionHandler>());
        container.Register<UnableToAuthenticateException>(provider.GetRequiredService<UnableToAuthenticateExceptionHandler>());
        container.Register<UnauthorizedAccessException>(provider.GetRequiredService<UnauthorizedAccessExceptionHandler>());

        return container;
    }
}
