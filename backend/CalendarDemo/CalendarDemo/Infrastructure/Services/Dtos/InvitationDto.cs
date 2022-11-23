using System;
using System.Collections.Generic;

namespace CalendarDemo.Infrastructure.Services.Dtos
{
    public class InvitationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? EventDateStart { get; set; }
        public DateTime? EventDateFinish { get; set; }
        public string InvitationUrl { get; set; }
        public string Title { get; set; }
        public Guid UserRequestId { get; set; }
        public IList<string> UserResponseIds { get; set; }
        public IList<string> ExternalEmails { get; set; }

        public IList<string> UserResponseEmails { get; set; }
    }
}
