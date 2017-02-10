using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ms_feedback.Models
{
    public class Feedback
    {
        //Feedback class defines the properties of feedback
        //these properties will be saved to a feedbacks table on Azure
        //each feedback will belong to a user
        public int ID { get; set; }
        public string SenderName { get; set; }
        public string FeedbackText { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool Anonymous { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        //public string UserId { get; set; }
    }
}
