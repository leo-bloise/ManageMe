namespace ManageMe.Application.DTOs;

public record RegisterUser(
    string Email,
    string Password,
    string Name
);
