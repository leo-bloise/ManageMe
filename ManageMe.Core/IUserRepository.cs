namespace ManageMe.Core;

public interface IUserRepository
{
    public bool ExistsByEmail(string email);

    public User? FindByEmail(string email);

    public User Save(User user);
}
