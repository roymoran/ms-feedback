using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ms_feedback.Models;

namespace ms_feedback.Data
{
    public class DbInitializer
    {
        public static void Initialize(UserFeedbackContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User { FullName = "Roy Moran", Email = "Roy.Moran@Microsoft.com", PasswordHash = "ExamplePassHash"}
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var feedbacks = new Feedback[]
            {
                new Feedback { UserID = 1, FeedbackText = "Great work on the bot project.", Anonymous = true, DeliveryDate = DateTime.Now},
                new Feedback { UserID = 1, FeedbackText = "Make sure you ask more questions on the calls.", Anonymous = false, SenderName = "Korey F.", DeliveryDate = DateTime.Now},
                new Feedback { UserID = 1, FeedbackText = "Awesome work on the project", Anonymous = true, DeliveryDate = DateTime.Now}
            };

            foreach (Feedback f in feedbacks)
            {
                context.Feedbacks.Add(f);
            }
            context.SaveChanges();
        }
    }
}
