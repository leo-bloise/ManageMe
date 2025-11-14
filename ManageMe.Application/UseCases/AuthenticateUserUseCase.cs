using ManageMe.Application.DTOs;
using ManageMe.Application.Exceptions;
using ManageMe.Application.Services;
using ManageMe.Core;

namespace ManageMe.Application.UseCases;

public class AuthenticateUserUseCase(IUserRepository userRepository, IPasswordEncoder passwordEncoder, ITokenFactory tokenFactory)
{
    public Token Execute(Credentials credentials)
    {
        User? user = userRepository.FindByEmail(credentials.Email);

        UnableToAuthenticateException.ThrowIfNull(user);

        if (!passwordEncoder.Verify(user!, credentials.Password)) throw new UnableToAuthenticateException();

        return tokenFactory.Create(user!);
    }
}
