using System.Security.Claims;

namespace ManageMe.Application;

public record Principal(int Id, string Email, string Name)
{
}
