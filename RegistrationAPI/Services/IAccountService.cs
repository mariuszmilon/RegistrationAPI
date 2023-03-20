using RegistrationAPI.Models.Account;
using RegistrationAPI.Models.User;

namespace RegistrationAPI.Services
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterUserDto dto);
        Task<UserDto> LoginAsync(LoginUserDto dto);
    }
}
