using System;
using System.Threading.Tasks;

namespace CalendarDemo.Infrastructure.Services.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
