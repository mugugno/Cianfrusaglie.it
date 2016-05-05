using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class FieldDefaultValue {
      public int Id { get; set; }
      [Required]
      public virtual string Value { get; set; }
      [Required]
      public virtual FormField FormField { get; set; }
   }
}