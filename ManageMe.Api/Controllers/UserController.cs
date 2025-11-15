using ManageMe.Api.Controllers.DTOs.Output;
using ManageMe.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

[Authorize]
public class UserController : ManageMeController
{
    [HttpGet("me")]
    public ActionResult Me()
    {
        Principal? principal = BuildPrincipal();

        if (principal is null) return new UnauthorizedObjectResult(null);

        return Ok(BaseApiResponse.WithData($"Hello, {principal.Name}", new Dictionary<string, dynamic>()
        {
            {"me",  principal}
        }));
    }
}
