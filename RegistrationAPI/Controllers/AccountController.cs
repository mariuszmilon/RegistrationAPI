using Microsoft.AspNetCore.Mvc;
using RegistrationAPI.Models;
using RegistrationAPI.Services;


namespace RegistrationAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto registerUserDto)
        {
            await _accountService.RegisterAsync(registerUserDto);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto loginUserDto)
        {
            return Ok(await _accountService.LoginAsync(loginUserDto));
        }
    }
}
