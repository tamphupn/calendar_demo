using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarDemo.Infrastructure.Entities;
using CalendarDemo.Infrastructure.Services.Dtos;

namespace CalendarDemo.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(string username, string password);
        Task<User> CreateAsync(UserDto user);
        IList<UserDto> Get();
    }
}
