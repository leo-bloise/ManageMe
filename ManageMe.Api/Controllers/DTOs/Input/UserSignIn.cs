namespace ManageMe.Api.Controllers.DTOs.Input;

public record UserSignIn(
    string Email,
    string Password,
    string Name
);
