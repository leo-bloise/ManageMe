using ManageMe.Core;
using System.ComponentModel.DataAnnotations;

namespace ManageMe.Api.Controllers.DTOs.Input;

public record TransactionDataInput(
    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Only transactions with, at least, 1 are considered")]
    decimal Amount,
    [Required]
    [MinLength(3)]
    string Description,
    [Required]
    Movement Movement,
    [Range(0, int.MaxValue)]
    int CategoryId
);
