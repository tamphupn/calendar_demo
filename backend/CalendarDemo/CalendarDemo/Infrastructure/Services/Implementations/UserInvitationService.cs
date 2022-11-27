using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalendarDemo.Infrastructure.EfCoreDbContext;
using CalendarDemo.Infrastructure.Entities;
using CalendarDemo.Infrastructure.Services.Dtos;
using CalendarDemo.Infrastructure.Services.Integrations;
using CalendarDemo.Infrastructure.Services.Interfaces;

namespace CalendarDemo.Infrastructure.Services.Implementations
{
    public class UserInvitationService : IUserInvitationService
    {
        private readonly CalendarDbContext _context;
        private readonly IGoogleCalendarIntegrationService _googleCalendarIntegrationService;

        public UserInvitationService(CalendarDbContext context, IGoogleCalendarIntegrationService googleCalendarIntegrationService)
        {
            _context = context;
            _googleCalendarIntegrationService = googleCalendarIntegrationService;
        }

        public async Task<bool> CreateInvitationAsync(InvitationDto invitation)
        {
            var newInvitation = new Invitation()
            {
                Id = Guid.NewGuid(),
                Name = invitation.Name,
                EventDateStart = invitation.EventDateStart,
                EventDateFinish = invitation.EventDateFinish
            };
            await _context.Invitations.AddAsync(newInvitation);

            if (invitation.ExternalEmails != null && invitation.ExternalEmails.Any())
            {
                invitation.ExternalEmails = invitation.ExternalEmails.Where(x => !string.IsNullOrEmpty(x)).ToList();
                foreach (var email in invitation.ExternalEmails)
                {
                    _context.UserInvitations.Add(new UserInvitation()
                    {
                        Id = Guid.NewGuid(),
                        UserId = invitation.UserRequestId,
                        InvitationId = newInvitation.Id,
                        ExternalEmail = email
                    });
                }
            }

            foreach (var userId in invitation.UserResponseIds)
            {
                _context.UserInvitations.Add(new UserInvitation()
                {
                    Id = Guid.NewGuid(),
                    UserId = invitation.UserRequestId,
                    InvitationId = newInvitation.Id,
                    InternalUserId = new Guid(userId)
                });
            }

            await _context.SaveChangesAsync();

            //TODO CALL GOOGLE EXTERNAL SERVICE TO CREATE CALENDAR
            invitation.UserResponseEmails = GetEmailUsers(invitation.UserResponseIds.Select(x => {
                return new Guid(x);
            }).ToList());

            var userRequest = _context.Users.FirstOrDefault(x => x.Id == invitation.UserRequestId);

            if (userRequest != null)
            {
                invitation.UserRequestEmail = userRequest.Email;
                await _googleCalendarIntegrationService.CreateGoogleCalendar(invitation);
                return true;
            }

            
            return false;
        }

        private IList<string> GetEmailUsers(IList<Guid> userIds)
        {
            return _context.Users.Where(x => userIds.Contains(x.Id)).Select(x => x.Email).ToList();
        }
    }
}
