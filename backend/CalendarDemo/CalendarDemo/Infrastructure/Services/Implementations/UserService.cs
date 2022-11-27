using System;
using System.Collections.Generic;
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

        public async Task<User> CreateAsync(UserDto user)
        {
            var existedUser = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (existedUser == null)
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
                return newUser;

            }
            return existedUser;    
        }

        public IList<UserDto> Get()
        {
            var query = _context.Users.Select(x => new UserDto()
            {
                Username = x.Username,
                Email = x.Email,
                Id = x.Id
            });

            return query.ToList();
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
