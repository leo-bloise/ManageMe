namespace ManageMe.Application;

public class Token
{
    private readonly string _token;

    public Token(string token)
    {
        _token = token;
    }

    public override string ToString()
    {
        return _token.ToString();
    }
}
