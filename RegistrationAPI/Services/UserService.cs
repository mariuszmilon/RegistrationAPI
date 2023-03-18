using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Entities;
using RegistrationAPI.Models;

namespace RegistrationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users =  await _dbContext.Users.AsNoTracking().ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
