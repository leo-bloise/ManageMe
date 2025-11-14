using ManageMe.Core;

namespace ManageMe.Infrastructure;

public class UserRepository(ManageMeContext manageMeContext) : IUserRepository
{
    public bool ExistsByEmail(string email)
    {
        return manageMeContext.Users.Any(u => u.Email == email);
    }

    public User? FindByEmail(string email)
    {
        return manageMeContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public User Save(User user)
    {
        manageMeContext.Users.Add(user);
        manageMeContext.SaveChanges();

        return user;
    }
}
