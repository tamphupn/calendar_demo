using CalendarDemo.Infrastructure.Services.Dtos;
using CalendarDemo.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,
        IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<bool> CreateAsync([FromBody] UserDto user)
        {
            try
            {
                return await _userService.CreateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController - CreateAsync: {ex.Message}");
                return false;
            }
        }

        [HttpGet]
        public async Task<UserDto> LoginAsync(string username, string password)
        {
            try
            {
                return await _userService.LoginAsync(username, password);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController - LoginAsync: {ex.Message}");
                return null;
            }
        }
    }
}
