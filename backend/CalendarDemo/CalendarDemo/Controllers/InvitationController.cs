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
    [Route("api/invitation")]
    public class InvitationController : ControllerBase
    {
        private readonly ILogger<InvitationController> _logger;
        private readonly IUserInvitationService _userInvitationService;

        public InvitationController(ILogger<InvitationController> logger,
        IUserInvitationService userInvitationService)
        {
            _logger = logger;
            _userInvitationService = userInvitationService;
        }

        [HttpPost("create")]
        public async Task<bool> CreateAsync([FromBody] InvitationDto invitation)
        {
            try
            {
                return await _userInvitationService.CreateInvitationAsync(invitation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"InvitationController - CreateAsync: {ex.Message}");
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> CreateTestAsync([FromBody] InvitationDto invitation)
        {
            try
            {
                return await _userInvitationService.CreateInvitationAsync(invitation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"InvitationController - CreateAsync: {ex.Message}");
                return false;
            }
        }
    }
}
