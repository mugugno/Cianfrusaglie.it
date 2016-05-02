using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Models
{
    public class User
    {
      public int Id { get; set; }

      [Required]
      [EmailAddress]
      public string Email { get; set; }

      [Required, MinLength(3), MaxLength(25)]
      public string NickName { get; set; }
      
      [DataType(DataType.DateTime)]
      public DateTime BirthDate { get; set; }

      [Required]
      public string City { get; set; } //TODO tabella con le città?!

      public virtual ICollection<User> BlockedUsers { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      public bool RememberMe { get; set; }
    }
}
