using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationAPI.Models.NewFolder;
using RegistrationAPI.Services;

namespace RegistrationAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsersAsync([FromQuery] PaginationQuery query)
        {
            return Ok(await _userService.GetUsersAsync(query));
        }
    }
}
