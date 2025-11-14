using ManageMe.Api.Controllers.DTOs.Input;
using ManageMe.Api.Controllers.DTOs.Output;
using ManageMe.Api.Filters;
using ManageMe.Application;
using ManageMe.Application.DTOs;
using ManageMe.Core;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

public class AuthController(RegisterUserUseCase registerUserUseCase, IApplicationLogger logger): ManageMeController
{
    [HttpPost("sign-in")]
    public ActionResult SignIn([FromBody] UserSignIn payload)
    {
        logger.Info("USER CREATE");

        User user = registerUserUseCase.Execute(new RegisterUser(payload.Email, payload.Password, payload.Name));

        return Created("/me", BaseApiResponse.WithData($"User with id {user.Id} created successfully", new()
        {
            { "user", new RegisteredUser(user.Id, user.Name, user.Email)}
        }));
    }
}
