using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ms_feedback.Models
{
    public class User
    {
        //User class defines the properties of the user
        //these properties will be saved to a user table on Azure
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public System.Guid ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
