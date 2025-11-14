using ManageMe.Api.Controllers.DTOs.Output;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Filters.Handlers;

public class UnableToAuthenticateExceptionHandler(ILogger<UnableToAuthenticateExceptionHandler> logger) : IExceptionHandler
{
    public IActionResult Handle(Exception exception)
    {
        logger.LogInformation(exception.Message);

        return new UnauthorizedObjectResult(
            BaseApiResponse.OnlyMessage(exception.Message)
        );
    }
}
