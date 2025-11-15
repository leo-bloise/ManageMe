using ManageMe.Core;
using ManageMe.Filters;
using System.Collections.Immutable;

namespace ManageMe.Infrastructure;

public class TransactionRepository(ManageMeContext context) : ITransactionRepository
{    
    public Transaction Create(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        context.SaveChanges();
        return transaction;
    }

    public DataPage<Transaction> GetPage(Principal principal, int page = 1, int size = 20)
    {
        int total = context.Transactions.Count(t => t.UserId.Equals(principal.Id));
        int totalPages = total / size;

        if (total % size != 0)
        {
            totalPages++;
        }

        IQueryable<Transaction> data = context.Transactions
            .Skip((page - 1) * size)
            .Take(size)
            .Where(t => t.UserId.Equals(principal.Id));

        return new DataPage<Transaction>(20, page, data.ToImmutableList());
    }

    public DataPage<Transaction> GetPage(Principal principal, DataFilterChain<Transaction> filterChain)
    {
        PageDataFilter<Transaction>? pageFilter = filterChain.OfType<PageDataFilter<Transaction>>().FirstOrDefault();

        if(pageFilter is null) throw new Exception("Page filter not defined while trying to retreive pages");

        IQueryable<Transaction> query = context.Transactions
            .Where(t => t.UserId == principal.Id)
            .OrderBy(t => t.Id);

        int total = 0;

        foreach (var filter in filterChain)
        {
            if(filter is PageDataFilter<Transaction>)
            {
                total = query.Count();
            }

            query = filter.Apply(query);
        }

        int totalPages = (total / pageFilter.Size);

        if (total % pageFilter.Size != 0) totalPages++;

        IEnumerable<Transaction> data = query.ToImmutableList();

        return new DataPage<Transaction>(totalPages, pageFilter.Page, data);
    }
}
