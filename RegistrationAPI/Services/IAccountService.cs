using RegistrationAPI.Models;

namespace RegistrationAPI.Services
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterUserDto dto);
        Task<UserDto> LoginAsync(LoginUserDto dto);
    }
}
