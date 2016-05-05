using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class Category {
      public int Id { get; set; }

      [Required, MinLength( 3 ), MaxLength( 25 )]
      public string Name { get; set; }

      public virtual Category OverCategory { get; set; }
      public virtual ICollection< Category > SubCategories { get; set; }
      public virtual ICollection< AnnounceCategory > CategoryAnnounces { get; set; }
      public virtual ICollection< CategoryFormField > CategoriesFormFields { get; set; }
   }
}