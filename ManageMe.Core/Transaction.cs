namespace ManageMe.Core;

public enum Movement
{
    OUTGOING,
    INCOMING,
}

public class Transaction
{
    public int Id { get; private set; }

    public decimal Amount { get; private set; }

    public string Description { get; private set; }

    public Movement Movement { get; private set; }

    public int UserId { get; private set; }

    public Transaction(int id, decimal amount, string description, Movement movement, User user)
    {
        Id = id;
        Amount = amount;
        Description = description;
        Movement = movement;
        UserId = user.Id;
    }
}
