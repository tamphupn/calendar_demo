using System;
using CalendarDemo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalendarDemo.Infrastructure.EfCoreDbContext
{
    public class CalendarDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<UserInvitation> UserInvitations { get; set; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
            : base(options)
        {
        }
    }
}
