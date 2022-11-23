using System;

namespace CalendarDemo.Infrastructure.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? EventDate { get; set; }
        public string InvitationUrl { get; set; }
        public string Title { get; set; }
    }
}
