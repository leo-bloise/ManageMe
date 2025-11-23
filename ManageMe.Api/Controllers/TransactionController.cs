using ManageMe.Api.Controllers.DTOs.Input;
using ManageMe.Api.Controllers.DTOs.Output;
using ManageMe.Application.UseCases;
using ManageMe.Core;
using ManageMe.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

[Authorize]
public class TransactionController(
    RegisterTransactionUseCase registerTransactionUseCase, 
    GetTransactionsUseCase getTransactionsUseCase
): ManageMeController
{
    [HttpPost]
    public ActionResult Create([FromBody] TransactionDataInput transactionData)
    {
        Principal principal = BuildPrincipal();

        Transaction transaction = registerTransactionUseCase.Register(new TransactionData(transactionData.Amount, transactionData.Description, transactionData.Movement, principal, transactionData.CategoryId));

        return Created($"/{transaction.Id}", BaseApiResponse.WithData("Transaction created successfully", new()
        {
            { "transaction", new TransactionDTO(transaction.Id, transaction.Description, transaction.Amount, transaction.Movement) }
        }));
    }

    [HttpGet]
    public ActionResult GetPage([FromQuery] int page = 1, [FromQuery] int size = 20)
    {
        Dictionary<string, string> queryParams = HttpContext.Request.Query
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

        if (!queryParams.ContainsKey("page") || !queryParams.ContainsKey("size")) 
        {
            queryParams["page"] = page.ToString();
            queryParams["size"] = size.ToString();
        }

        DataFilterTokenCollection dataFilterTokenCollection = new DataFilterTokenCollection(queryParams);

        DataPage<Transaction> transactionsPage = getTransactionsUseCase.Execute(dataFilterTokenCollection, BuildPrincipal());

        return Ok(BaseApiResponse.WithData("Transactions", new()
        {
            {"page", transactionsPage}
        }));
    }
}
