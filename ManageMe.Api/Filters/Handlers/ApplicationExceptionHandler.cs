using ManageMe.Api.Controllers.DTOs.Output;
using ManageMe.Application;

namespace ManageMe.Api.Filters.Handlers;

public class ApplicationExceptionHandler(IApplicationLogger logger): IExceptionHandler
{
    public BaseApiResponse Handle(Exception exception)
    {
        logger.Error(exception.Message);

        string message = exception.Message;
        return BaseApiResponse.OnlyMessage(message);
    }
}
