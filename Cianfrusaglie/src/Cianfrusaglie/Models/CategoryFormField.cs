using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cianfrusaglie.Models {
   public class CategoryFormField {
	   
	  [ForeignKey("Category")]
      public int CategoryId { get; set; }
      public virtual Category Category { get; set; }
      
      [ForeignKey("FormField")]
      public int FormFieldId { get; set; }
      public virtual FormField FormField { get; set; }
   }
}
