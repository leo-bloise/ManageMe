using ManageMe.Api.Filters;
using ManageMe.Application.Exceptions;
using ManageMe.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ManageMe.Api.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter<ExceptionFilter>]
public abstract class ManageMeController: ControllerBase
{
    protected Principal BuildPrincipal()
    {
        if(User is null)
        {
            throw new UnauthorizedAccessException();
        }

        string? email = User.FindFirst(ClaimTypes.Email)?.Value;
        string? name = User.FindFirst(ClaimTypes.Name)?.Value;
        string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(email is null || name is null || id is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        if(!int.TryParse(id, out int idParse))
        {
            throw new UnauthorizedAccessException();
        }

        return new Principal(idParse, email, name);
    }
}
