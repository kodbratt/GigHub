using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitHub.Core.Models;
using GitHub.Persistence;

namespace GigHub.Core.Models
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public IEnumerable<UserNotification> GetUserNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
        }
    }
}