using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cianfrusaglie.Models {
   // Add profile data for application users by adding properties to the User class
   public class User : IdentityUser {
      [DataType( DataType.DateTime )]
      public DateTime BirthDate { get; set; }

      [Required]
      public virtual GeoCoordinateEntity GeoCoordinate { get; set; }

      public bool RememberMe { get; set; }

      public virtual ICollection< Announce > PublishedAnnounces { get; set; }
      public virtual ICollection< Interested > InterestedAnnounces { get; set; }
      public virtual ICollection< User > BlockedUsers { get; set; }
      public virtual ICollection< Message > SentMessages { get; set; }
      public virtual ICollection< Message > ReceivedMessages { get; set; }
      public virtual ICollection< FeedBack > SentFeedBacks { get; set; }
      public virtual ICollection< FeedBack > ReceivedFeedBacks { get; set; }
   }
}