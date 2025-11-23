using System.Security.Claims;

namespace ManageMe.Core;

public class User
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Password { get; private set; }

    public string Email { get; private set; }

    public virtual ICollection<Transaction> Transactions { get; private set; }

    public virtual ICollection<Category> Categories { get; private set; }

    public User(int id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    public User(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = "";
    }

    public void SetPassword(string password)
    {
        Password = password;
    }
}
