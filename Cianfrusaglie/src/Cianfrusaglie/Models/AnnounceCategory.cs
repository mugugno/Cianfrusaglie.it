using System.ComponentModel.DataAnnotations.Schema;

namespace Cianfrusaglie.Models {
   public class AnnounceCategory {
      [ForeignKey( "Announce" )]
      public int AnnounceId { get; set; }

      public virtual Announce Announce { get; set; }

      [ForeignKey( "Category" )]
      public int CategoryId { get; set; }

      public virtual Category Category { get; set; }
   }
}