namespace ManageMe.Application;

public interface IApplicationLogger
{
    public void Info(string message);

    public void Error(string message);

    public void Warn(string message);
}
