using ManageMe.Core;
using System.Security.Claims;

namespace ManageMe.Application.Services;

public interface ITokenFactory
{
    public Token Create(User user);

}
