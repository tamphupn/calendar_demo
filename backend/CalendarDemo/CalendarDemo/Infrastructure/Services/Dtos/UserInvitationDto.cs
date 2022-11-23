using System;

namespace CalendarDemo.Infrastructure.Services.Dtos
{
    public class UserInvitationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? InvitationId { get; set; }
    }
}
