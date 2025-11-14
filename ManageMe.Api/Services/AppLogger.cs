using ManageMe.Application;

namespace ManageMe.Api.Services;

public class AppLogger(ILogger<AppLogger> logger) : IApplicationLogger
{
    public void Error(string message)
    {
        logger.LogError(message);
    }

    public void Info(string message)
    {
        logger.LogInformation(message);
    }

    public void Warn(string message)
    {
        logger.LogWarning(message);
    }
}
