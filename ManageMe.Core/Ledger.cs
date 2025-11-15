using ManageMe.Core.Filters;
using ManageMe.Filters;

namespace ManageMe.Core;

public class Ledger
{
    private readonly ITransactionRepository _transactionRepository;

    private readonly Calendar _calendar;

    public Ledger(ITransactionRepository transactionRepository, Calendar calendar)
    {
        _transactionRepository = transactionRepository;
        _calendar = calendar;
    }

    public async Task<decimal> CalculateMonthBalance(Principal principal)
    {
        DateTime firstDayOfMonth = _calendar.FirstDayOfMonth(DateTime.Now);
        DateTime lastDayOfMonth = _calendar.LastDayOfMonth(DateTime.Now);

        Task<decimal> Sum(DataPage<Transaction> page) 
        {
            decimal total = decimal.Zero;

            foreach(Transaction transaction in page.Data)
            {
                if(transaction.Movement == Movement.OUTGOING)
                {
                    total -= transaction.Amount;
                    continue;
                }

                total += transaction.Amount;
            }

            return Task.FromResult(total);
        }

        IList<Task<decimal>> sumTasks = new List<Task<decimal>>();

        int page = 1;
        int totalPages = int.MaxValue;

        do
        {
            DataFilterTokenCollection collection = new DataFilterTokenCollection(new()
            {
                {"page", page.ToString() },
                {"size", "100" },
                {"start", firstDayOfMonth.ToString()},
                {"end", lastDayOfMonth.ToString()},
            });

            DataFilterChainTransactionBuilder builder = new DataFilterChainTransactionBuilder(collection);

            DataPage<Transaction> dataPage = _transactionRepository.GetPage(principal, builder.Build());

            totalPages = dataPage.TotalCount;
            page = dataPage.Page;

            sumTasks.Add(Sum(dataPage));

        } while (page < totalPages);

        decimal[] amounts = await Task.WhenAll(sumTasks);

        decimal totalBalance = decimal.Zero;

        foreach (decimal amount in amounts)
        {
            totalBalance += amount;
        }

        return totalBalance;
    }
}
