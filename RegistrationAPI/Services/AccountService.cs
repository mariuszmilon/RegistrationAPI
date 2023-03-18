using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Entities;
using RegistrationAPI.Exepctions;
using RegistrationAPI.Models;
using System.Security.Claims;

namespace RegistrationAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(ApplicationDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> LoginAsync(LoginUserDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == dto.Email) ?? throw new BadRequestException("Invalid email or password!");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid email or password!");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

            return _mapper.Map<UserDto>(user);
        }

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
