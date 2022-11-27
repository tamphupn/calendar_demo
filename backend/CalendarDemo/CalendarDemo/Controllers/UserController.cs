using CalendarDemo.Infrastructure.Services.Dtos;
using CalendarDemo.Infrastructure.Services.Integrations;
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
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IGoogleCalendarIntegrationService _googleCalendarIntegrationService;

        public UserController(ILogger<UserController> logger,
        IUserService userService,
        IGoogleCalendarIntegrationService googleCalendarIntegrationService)
        {
            _logger = logger;
            _userService = userService;
            _googleCalendarIntegrationService = googleCalendarIntegrationService;
        }

        [HttpPost]
        public async Task<bool> CreateAsync([FromBody] UserDto user)
        {
            try
            {
                var userCreated = await _userService.CreateAsync(user);

                var credential = _googleCalendarIntegrationService.LoginOauth2(userCreated.Id, userCreated.Email);
                return true;
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

        [HttpGet("user-dropdown")]
        public  IList<UserDto> Get()
        {
            try
            {
                return _userService.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController - Get: {ex.Message}");
                return null;
            }
        }
    }
}
