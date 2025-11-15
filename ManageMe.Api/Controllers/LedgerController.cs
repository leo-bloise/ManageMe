using ManageMe.Api.Controllers.DTOs.Output;
using ManageMe.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

[Authorize]
public class LedgerController(CalculateBalanceOfMonthUseCase calculateBalanceOfMonthUseCase): ManageMeController
{
    [HttpGet("month-balance")]
    public async Task<ActionResult> MonthBalance()
    {
        decimal balance = await calculateBalanceOfMonthUseCase.Execute(BuildPrincipal());

        return Ok(BaseApiResponse.WithData("Balance of the month", new()
        {
            { "balance",  balance }
        }));
    }
}
