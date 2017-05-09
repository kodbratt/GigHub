using System;
using System.ComponentModel.DataAnnotations;


namespace GitHub.Core.Models
{
    public class Notification
    {
        protected Notification()
        {

        }
    
        public int Id { get;private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get;private set; }

        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException("Gig is null");

            Gig = gig;
            DateTime = DateTime.Now;
            Type = type;

        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig,NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(newGig, NotificationType.GigUpdated)
            {
                OriginalDateTime = originalDateTime,
                OriginalVenue = originalVenue
            };
            return notification;
        }

        public static Notification GigCancel(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }
    }
}