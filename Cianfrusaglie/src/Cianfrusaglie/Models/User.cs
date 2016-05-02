using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class User {
      public int Id { get; set; }

      [Required]
      public string Email { get; set; }

      [Required, MinLength( 3 ), MaxLength( 13 )]
      public string NickName { get; set; }

      public DateTime BirthDate { get; set; }

      [Required]
      public string City { get; set; } //TODO tabella con le città?!
      public virtual ICollection< User > BlockedUsers { get; set; }
   }
}