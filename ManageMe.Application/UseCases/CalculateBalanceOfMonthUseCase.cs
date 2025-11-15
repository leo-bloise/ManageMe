using ManageMe.Core;

namespace ManageMe.Application.UseCases;

public class CalculateBalanceOfMonthUseCase(Ledger ledger)
{
    public Task<decimal> Execute(Principal principal)
    {
        return ledger.CalculateMonthBalance(principal);
    }
}
