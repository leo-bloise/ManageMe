using ManageMe.Api.Controllers.DTOs.Output;
using ManageMe.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

[Authorize]
public class UserController : ManageMeController
{
    [HttpGet("me")]
    public ActionResult Me()
    {
        Principal principal = BuildPrincipal();

        return Ok(BaseApiResponse.WithData($"Hello, {principal.Name}", new Dictionary<string, dynamic>()
        {
            {"me",  principal}
        }));
    }
}
