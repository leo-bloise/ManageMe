using ManageMe.Filters;

namespace ManageMe.Core;

public interface ITransactionRepository
{
    public Transaction Create(Transaction transaction);

    public DataPage<Transaction> GetPage(Principal principal, DataFilterChain<Transaction> filterChain);
}
