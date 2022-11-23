using System;
using System.Linq;
using System.Threading.Tasks;
using CalendarDemo.Infrastructure.EfCoreDbContext;
using CalendarDemo.Infrastructure.Entities;
using CalendarDemo.Infrastructure.Services.Dtos;
using CalendarDemo.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CalendarDemo.Infrastructure.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CalendarDbContext _context;

        public UserService(CalendarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(UserDto user)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Password = user.Password,
                Email = user.Email
            };
            await _context.Users.AddAsync(newUser);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserDto> LoginAsync(string username, string password)
        {
            username = username.Trim();
            password = password.Trim();
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            return new UserDto()
            {
                Username = user.Username,
                Email = user.Email,
                Id = user.Id,
            };
        }
    }
}
