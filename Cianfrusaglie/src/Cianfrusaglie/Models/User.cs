﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cianfrusaglie.Models {
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser {
        [DataType( DataType.DateTime )]
        public DateTime BirthDate { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }

        /// <summary>
        /// 0 -> non specificato; 1 -> donna; 2 -> uomo
        /// </summary>
        [Range(DomainConstraints.UserGenreNotSpecified, DomainConstraints.UserGenreMale)]
        public virtual int Genre { get; set; }

        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }

        public bool RememberMe { get; set; }

        [MaxLength(DomainConstraints.ImageUrlUrlMaxLenght)]
        public virtual string ProfileImageUrl { get; set; }

        public virtual ICollection< Announce > PublishedAnnounces { get; set; }
        public virtual ICollection< Interested > InterestedAnnounces { get; set; }
        public virtual ICollection< User > BlockedUsers { get; set; }
        public virtual ICollection< Message > SentMessages { get; set; }
        public virtual ICollection< Message > ReceivedMessages { get; set; }
        public virtual ICollection< FeedBack > SentFeedBacks { get; set; }
        public virtual ICollection< FeedBack > ReceivedFeedBacks { get; set; }
        public virtual ICollection< AnnounceChosen > ChosenUsers { get; set; }
    }
}