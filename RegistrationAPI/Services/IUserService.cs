using RegistrationAPI.Models.NewFolder;
using RegistrationAPI.Models.User;

namespace RegistrationAPI.Services
{
    public interface IUserService
    {
        Task<PaginationResult<UserDto>> GetUsersAsync(PaginationQuery query);
    }
}
