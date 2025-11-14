using ManageMe.Core;

namespace ManageMe.Application;

public interface IPasswordEncoder
{
    public string HashPassword(User user);

    public bool Verify(User user, string plain);
}
