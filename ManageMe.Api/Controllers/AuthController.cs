using ManageMe.Api.Controllers.DTOs.Input;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

public class AuthController: ManageMeController
{
    [HttpPost("sign-in")]
    public ActionResult SignIn([FromBody] UserSignIn payload)
    {
        return Ok(payload);
    }
}
