using System;
using System.Threading.Tasks;
using CalendarDemo.Infrastructure.Services.Dtos;
using Google.Apis.Calendar.v3.Data;

namespace CalendarDemo.Infrastructure.Services.Integrations
{
    public interface IGoogleCalendarIntegrationService
    {
        Task<Event> CreateGoogleCalendar(InvitationDto request);
    }
}
