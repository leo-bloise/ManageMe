using ManageMe.Core;

namespace ManageMe.Application.UseCases;

public record TransactionData(
    decimal Amount,
    string Description,
    Movement Movement,
    Principal Principal
    );

public class RegisterTransactionUseCase(ITransactionRepository transactionRepository)
{
    public Transaction Register(TransactionData data)
    {
        Transaction transaction = new Transaction(
            0,
            data.Amount,
            data.Description,
            data.Movement,
            data.Principal.Id,
            DateTime.Now
        );

        return transactionRepository.Create(transaction);
    } 
}
