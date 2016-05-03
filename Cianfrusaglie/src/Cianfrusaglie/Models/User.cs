using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cianfrusaglie.Models {
    public class User : IdentityUser {
        public int Id { get; set; }

        [Required, EmailAddress, MinLength( 7 ), MaxLength( 30 )]
        public string Email { get; set; }

        [Required, MinLength( 3 ), MaxLength( 25 )]
        public string NickName { get; set; }

        [DataType( DataType.DateTime )]
        public DateTime BirthDate { get; set; }

        public string City { get; set; } //TODO tabella con le città?!

        public virtual ICollection< User > BlockedUsers { get; set; }

        [Required, DataType( DataType.Password ), MinLength( 3 )]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public virtual ICollection<Message> SendedMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }  
    }
}