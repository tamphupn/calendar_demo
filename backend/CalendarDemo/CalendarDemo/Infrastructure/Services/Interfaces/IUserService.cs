using System;
using System.Threading.Tasks;
using CalendarDemo.Infrastructure.Services.Dtos;

namespace CalendarDemo.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(string username, string password);
        Task<bool> CreateAsync(UserDto user);
    }
}
