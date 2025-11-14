using ManageMe.Api.Controllers.DTOs.Output;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Filters.Handlers;

public class GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger): IExceptionHandler
{
    public IActionResult Handle(Exception exception)
    {
        logger.LogDebug($"[PANIC] Unknown exception thrown {exception.Message} {exception.StackTrace}");

        logger.LogCritical($"[PANIC] Unknown exception thrown. Please, check the code.");

        return new ObjectResult(BaseApiResponse.OnlyMessage(""))
        {
            StatusCode = 500
        };
    }
}
