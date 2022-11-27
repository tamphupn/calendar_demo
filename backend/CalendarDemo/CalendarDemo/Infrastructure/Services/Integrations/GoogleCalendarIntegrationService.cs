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
using Microsoft.Extensions.Caching.Memory;

namespace CalendarDemo.Infrastructure.Services.Integrations
{
    public class GoogleCalendarIntegrationService : IGoogleCalendarIntegrationService
    {
        private IMemoryCache _cache;

        public GoogleCalendarIntegrationService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<Event> CreateGoogleCalendar(InvitationDto request)
        {
            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
            string ApplicationName = "Google Calendar Api";
            var cacheCredential = GetTokenToUse(request.UserRequestId);
            UserCredential credential = cacheCredential == null ? await LoginOauth2(request.UserRequestId, request.UserRequestEmail) : cacheCredential;

            // define services
            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var attendees = request.UserResponseEmails.Select(email =>
            {
                return new EventAttendee() { Email = email, DisplayName = email };
            });

            if (request.ExternalEmails != null && request.ExternalEmails.Any())
            {
                foreach (var email in request.ExternalEmails)
                {
                    attendees = attendees.Append(new EventAttendee()
                    {
                        Email = email,
                        DisplayName = email
                    });
                }
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
                Description = request.Name,
                Attendees = attendees.ToList()
                
            };
            var eventRequest = services.Events.Insert(eventCalendar, "primary");
            eventRequest.SendNotifications = true;
            var requestCreate = await eventRequest.ExecuteAsync();
            return requestCreate;
        }

        public async Task<UserCredential> LoginOauth2(Guid userLoginId, string user)
        {
            string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
            UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "token.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = Path.Combine(Directory.GetCurrentDirectory(), "Credentials", $"{userLoginId}");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds((double)credential.Token.ExpiresInSeconds));

            _cache.Set(userLoginId, credential, cacheEntryOptions);
            return credential;
        }

        private UserCredential GetTokenToUse(Guid userLoginId)
        {
            var credental = _cache.Get(userLoginId);
            if (credental == null) return null;

            return (UserCredential)credental;
        }
    }
}
