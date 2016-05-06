using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class Gat {
      public int Id { get; set; }

      [Required, MinLength( 3 ), MaxLength( 25 )]
      public virtual string Text { get; set; }

      public virtual ICollection< AnnounceGat > AnnouncesGats { get; set; }
   }
}