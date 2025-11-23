using System.ComponentModel.DataAnnotations;

namespace ManageMe.Api.Controllers.DTOs.Input;

public record UserSignIn(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    string Email,
    [Required(ErrorMessage = "Password is required")]
    [MinLength(3, ErrorMessage = "Password must have, at least, 3 chars")]
    string Password,
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must have, at least, 3 chars")]
    string Name
);
