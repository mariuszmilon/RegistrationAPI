using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationAPI.Entities;
using RegistrationAPI.Models.NewFolder;
using RegistrationAPI.Models.User;
using System.Linq.Expressions;

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

        public async Task<PaginationResult<UserDto>> GetUsersAsync(PaginationQuery query)
        {
            var users = _dbContext.Users.Where(r => (query.SearchString == null) //allowed searching by name, surname, email and personal id number
                || (r.Name.ToLower().Contains(query.SearchString.ToLower())
                || r.Surname.ToLower().Contains(query.SearchString.ToLower())
                || r.Email.ToLower().Contains(query.SearchString.ToLower())
                || r.PersonalIdNumber.ToLower().Contains(query.SearchString.ToLower())));


            if (!string.IsNullOrEmpty(query.OrderBy)) // default ascending sort
            {
                users = query.Descending == true 
                    ? users.OrderByDescending(SortColumnSelector(query.OrderBy.ToLower()))
                    : users.OrderBy(SortColumnSelector(query.OrderBy.ToLower()));
            }

            var result = await users
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            return new PaginationResult<UserDto>(_mapper.Map<List<UserDto>>(result), users.Count(), query.PageSize, query.PageNumber);
        }

        private Expression<Func<User, object>> SortColumnSelector(string key) => key switch //allowed sorting by Name, Surname, DateOfBirth and AveragePowerConsumption
        {
            "name" => p => p.Name,
            "surname" => p => p.Surname,
            "dateofbirth" => p => p.DateOfBirth,
            _ => p => p.AveragePowerConsumption
        };
    }
}
