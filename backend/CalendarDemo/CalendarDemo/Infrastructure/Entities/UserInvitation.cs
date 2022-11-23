using System;

namespace CalendarDemo.Infrastructure.Entities
{
    public class UserInvitation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid InvitationId { get; set; }
        public Guid? InternalUserId { get; set; }
        public string ExternalEmail { get; set; }
    }
}
