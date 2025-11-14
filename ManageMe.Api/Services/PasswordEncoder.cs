using ManageMe.Application;
using ManageMe.Core;
using Microsoft.AspNetCore.Identity;

namespace ManageMe.Api.Services;

public class PasswordEncoder(IPasswordHasher<User> passwordHasher): IPasswordEncoder
{
    public string HashPassword(User user) => passwordHasher.HashPassword(user, user.Password);

    public bool Verify(User user, string plain) => passwordHasher.VerifyHashedPassword(user, user.Password, plain) == PasswordVerificationResult.Success;    
}
