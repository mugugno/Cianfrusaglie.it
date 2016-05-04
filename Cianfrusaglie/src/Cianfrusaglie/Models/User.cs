using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cianfrusaglie.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser {
        [DataType( DataType.DateTime )]
        public DateTime BirthDate { get; set; }

        public string City { get; set; } //TODO tabella con le città?!

        public bool RememberMe { get; set; }

      public virtual ICollection<Announce> PublishedAnnounces { get; set; }

      public virtual ICollection<User> BlockedUsers { get; set; }
      public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
    }
}
