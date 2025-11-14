using ManageMe.Api.Filters.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManageMe.Api.Filters;

public class ExceptionFilter: IExceptionFilter
{
    private readonly HandlerContainer _handlerContainer;

    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(IServiceProvider provider, ILogger<ExceptionFilter> logger)
    {
        _handlerContainer = Initialize(provider);
        _logger = logger;
    }

    private HandlerContainer Initialize(IServiceProvider provider)
    {
        return HandlerContainer.WithDefaults(provider);
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        _logger.LogInformation($"Exception captured - {exception.GetType().Name}");

        var handler = _handlerContainer.ForException(exception.GetType());

        var response = handler.Handle(exception);

        context.Result = new UnprocessableEntityObjectResult(response);
    }
}
