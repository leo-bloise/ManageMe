using ManageMe.Application.DTOs;
using ManageMe.Core;

namespace ManageMe.Application;

public class RegisterUserUseCase(
    IUserRepository userRepository,
    IPasswordEncoder passwordEncoder
) {
    public User Execute(RegisterUser registerUser)
    {
        if (userRepository.ExistsByEmail(registerUser.Email)) throw new AppException($"Email {registerUser.Email} already in use.");        

        User user = new User(
            0,
            registerUser.Name,
            registerUser.Email,
            registerUser.Password
        );

        user.SetPassword(passwordEncoder.HashPassword(user));

        return userRepository.Save(user);
    }
}
