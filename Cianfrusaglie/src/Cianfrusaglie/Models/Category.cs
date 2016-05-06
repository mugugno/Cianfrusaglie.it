using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cianfrusaglie.Models {
   public class Category {
      public int Id { get; set; }

      [Required, Column(TypeName = "varchar(99)")]
      public string Name { get; set; }

      public virtual Category OverCategory { get; set; }
      public virtual ICollection< Category > SubCategories { get; set; }
      public virtual ICollection< AnnounceCategory > CategoryAnnounces { get; set; }
      public virtual ICollection< CategoryFormField > CategoriesFormFields { get; set; }
   }
}