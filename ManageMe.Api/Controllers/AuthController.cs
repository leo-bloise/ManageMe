using ManageMe.Api.Controllers.DTOs.Input;
using ManageMe.Api.Controllers.DTOs.Output;
using ManageMe.Application;
using ManageMe.Application.DTOs;
using ManageMe.Application.UseCases;
using ManageMe.Core;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

public class AuthController(AuthenticateUserUseCase authenticateUserUseCase, RegisterUserUseCase registerUserUseCase, ILogger<AuthController> logger): ManageMeController
{
    [HttpPost("sign-in")]
    public ActionResult SignIn([FromBody] UserSignIn payload)
    {
        logger.LogInformation("USER CREATE");

        User user = registerUserUseCase.Execute(new RegisterUser(payload.Email, payload.Password, payload.Name));

        return Created("/user/me", BaseApiResponse.WithData($"User with id {user.Id} created successfully", new()
        {
            { "user", new RegisteredUser(user.Id, user.Name, user.Email)}
        }));
    }

    [HttpPost]
    public ActionResult Authenticate([FromBody] CredentialsInput credentials)
    {
        logger.LogInformation("USER AUTHENTICATE");

        Token token = authenticateUserUseCase.Execute(new Credentials(credentials.Email, credentials.Password));

        return Created("/user/me", BaseApiResponse.WithData("Authenticated successfuly", new Dictionary<string, dynamic>
        {
            {"token", token.ToString() }
        }));
    }
}
