namespace ManageMe.Api.Controllers.DTOs.Input;

public record UserSignIn(
    string email,
    string password,
    string name
);
