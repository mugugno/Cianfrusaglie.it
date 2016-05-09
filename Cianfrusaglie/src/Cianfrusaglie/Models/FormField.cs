using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class FormField {
      public int Id { get; set; }

      [Required, MinLength( 3 ), MaxLength( 30 )]
      public virtual string Name { get; set; }

      [Required]
      public virtual FormFieldType Type { get; set; }

      //Da decidere se fare enum, vale S,D,IM,I(Stringa,Double,Immagine,Integer)

      public virtual ICollection< FieldDefaultValue > DefaultValues { get; set; }
      public virtual ICollection< CategoryFormField > CategoriesFormFields { get; set; }
      public virtual ICollection< AnnounceFormFieldsValues > AnnouncesFormFields { get; set; }
   }
}