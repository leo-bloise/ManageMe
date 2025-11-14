using ManageMe.Core;

namespace ManageMe.Application.Exceptions;

public class UnableToAuthenticateException(): AppException("Unable to authenticate")
{
    public static void ThrowIfNull(object? user)
    {
        if (user is null) throw new UnableToAuthenticateException();
    }
}
