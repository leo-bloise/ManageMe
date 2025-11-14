using ManageMe.Application.Exceptions;

namespace ManageMe.Api.Filters.Handlers;

public class HandlerContainer(ILogger<HandlerContainer> logger)
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
        
        if (!metadata.IsAssignableTo(typeof(Exception))) throw new Exception($"Type {metadata.Name} must be an Exception");

        logger.LogDebug($"Handler for {metadata.Name} retreived");
        return _values[metadata];
    }

    public static HandlerContainer WithDefaults(IServiceProvider provider)
    {
        HandlerContainer container = new HandlerContainer(provider.GetRequiredService<ILogger<HandlerContainer>>());

        container.Register<Exception>(provider.GetRequiredService<GeneralExceptionHandler>());
        container.Register<AppException>(provider.GetRequiredService<ApplicationExceptionHandler>());
        container.Register<UnableToAuthenticateException>(provider.GetRequiredService<UnableToAuthenticateExceptionHandler>());


        return container;
    }
}
