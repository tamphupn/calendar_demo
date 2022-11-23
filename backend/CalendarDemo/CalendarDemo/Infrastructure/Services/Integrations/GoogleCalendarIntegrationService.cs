using System;
using System.Linq;
using System.Threading.Tasks;
using CalendarDemo.Infrastructure.EfCoreDbContext;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using CalendarDemo.Infrastructure.Services.Dtos;

namespace CalendarDemo.Infrastructure.Services.Integrations
{
    public class GoogleCalendarIntegrationService : IGoogleCalendarIntegrationService
    {
        public async Task<Event> CreateGoogleCalendar(InvitationDto request)
        {
            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
            string ApplicationName = "Google Calendar Api";
            UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Cre", "cre.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            // define services
            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var attendees = request.UserResponseEmails.Select(email =>
            {
                return new EventAttendee() { Email = email };
            });

            foreach (var email in request.ExternalEmails)
            {
                attendees.Append(new EventAttendee() {
                    Email = email
                });
            }

            // define request
            Event eventCalendar = new Event()
            {
                Summary = request.Title,
                Location = request.Title,
                Start = new EventDateTime
                {
                    DateTime = request.EventDateStart,
                    TimeZone = "Asia/Ho_Chi_Minh"
                },
                End = new EventDateTime
                {
                    DateTime = request.EventDateFinish,
                    TimeZone = "Asia/Ho_Chi_Minh"
                },
                Description = request.Title,
                Attendees = attendees.ToList(),
                
            };
            var eventRequest = services.Events.Insert(eventCalendar, "primary");
            var requestCreate = await eventRequest.ExecuteAsync();
            return requestCreate;
        }
    }
}
