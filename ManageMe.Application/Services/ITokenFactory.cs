using ManageMe.Core;

namespace ManageMe.Application.Services;

public interface ITokenFactory
{
    public Token Create(User user);
}
