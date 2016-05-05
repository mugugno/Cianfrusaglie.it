using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class FormField {
      public int Id { get; set; } 
      [Required, MinLength( 3 ), MaxLength( 30 )]
      public virtual string Name { get; set; }
      
      //Fatto ClaFranck
      [Required]
      public virtual string Type { get; set; }//Da decidere se fare enum, vale S,D,IM,I(Stringa,Double,Immagine,Integer)
      
      //public virtual ICollection< String > DefaultValues { get; set; }
      public virtual ICollection< CategoryFormField > CategoriesFormFields { get; set; } 
      //public virtual ICollection<AnnounceFormField> AnnouncesFormFields { get; set; }

   }
}
