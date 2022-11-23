using System;
using System.Threading.Tasks;
using CalendarDemo.Infrastructure.Services.Dtos;

namespace CalendarDemo.Infrastructure.Services.Interfaces
{
    public interface IUserInvitationService
    {
        Task<bool> CreateInvitationAsync(InvitationDto invitation);
    }
}
