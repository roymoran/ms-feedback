using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ms_feedback.Models;
using Microsoft.EntityFrameworkCore;

namespace ms_feedback.Data
{
    public class UserFeedbackContext : DbContext
    {
        public UserFeedbackContext(DbContextOptions<UserFeedbackContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
