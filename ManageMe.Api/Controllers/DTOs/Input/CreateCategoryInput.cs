using System.ComponentModel.DataAnnotations;

namespace ManageMe.Api.Controllers.DTOs.Input;

public record CreateCategoryInput(
    [Required]
    [MinLength(3)]
    [MaxLength(25)]
    string Name
);
