using ManageMe.Application.Exceptions;
using ManageMe.Core;
using ManageMe.Core.Exceptions;
using ManageMe.Core.Filters;
using ManageMe.Filters;

namespace ManageMe.Application.UseCases;

public class GetTransactionsUseCase(ITransactionRepository transactionRepository)
{
    private void ValidatePageAndSize(DataFilterTokenCollection collection)
    {
        string? page = collection.GetTokenValue("page") as string;
        string? size = collection.GetTokenValue("size") as string;

        if (string.IsNullOrEmpty(page) || string.IsNullOrEmpty(size)) throw new AppException("Page and Size must be provided to paginate transactions");

        if (!int.TryParse(page, out int pageValue))
        {
            throw new InvalidFilterValue("page", "page must be an int");
        }

        if(pageValue <= 0)
        {
            throw new InvalidFilterValue("page", "page must be bigger than 0");
        }

        if (!int.TryParse(size, out int sizeValue))
        {
            throw new InvalidFilterValue("size", "size must be an int");
        }

        if (sizeValue <= 0)
        {
            throw new InvalidFilterValue("size", "size must be bigger than 0");
        }
    }

    public DataPage<Transaction> Execute(DataFilterTokenCollection dataFilterTokenCollection, Principal principal)
    {
        ValidatePageAndSize(dataFilterTokenCollection);

        DataFilterChainBuilder<Transaction> dataFilterChainBuilder = new DataFilterChainTransactionBuilder(dataFilterTokenCollection);

        DataFilterChain<Transaction> dataFilterChain = dataFilterChainBuilder.Build();

        return transactionRepository.GetPage(principal, dataFilterChain);
    }
}
