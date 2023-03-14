using RegistrationAPI.Models;

namespace RegistrationAPI.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
    }
}
