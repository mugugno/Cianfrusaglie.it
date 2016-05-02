using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class Category {
      public int Id { get; set; }

      [Required, MinLength( 3 )]
      public string Name { get; set; }

      public virtual Category OverCategory { get; set; }
      public virtual ICollection< Category > SubCategories { get; set; }
   }
}