using ManageMe.Core;

namespace ManageMe.Api.Controllers.DTOs.Output;

public record TransactionDTO(int Id, string Description, decimal Amount, Movement Movement);
