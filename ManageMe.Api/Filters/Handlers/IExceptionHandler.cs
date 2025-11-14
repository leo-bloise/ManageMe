using ManageMe.Api.Controllers.DTOs.Output;

namespace ManageMe.Api.Filters.Handlers;

public interface IExceptionHandler
{
    public BaseApiResponse Handle(Exception exception);
}
