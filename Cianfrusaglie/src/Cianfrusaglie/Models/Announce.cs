using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Device.Location;

namespace Cianfrusaglie.Models {
   public class Announce {
      public int Id { get; set; }

      [Required]
      public virtual User Author { get; set; }

      [Required]
      public virtual DateTime PublishDate { get; set; }

      [Required, MinLength( 3 ), MaxLength( 50 )]
      public virtual string Title { get; set; }

      [Required, MinLength( 10 ), MaxLength( 255 )]
      public virtual string Description { get; set; }

      public virtual ICollection< ImageUrl > Images { get; set; } 

      public virtual bool Closed { get; set; }

      [Required]
      public virtual string City { get; set; } //TODO tabella città...

      [Range( 0, int.MaxValue )]
      public virtual int Range { get; set; } //TODO in metri?!

      public virtual ICollection< AnnounceCategory > AnnounceCategories { get; set; }

      public virtual ICollection< AnnounceGat > AnnouncesGats { get; set; }
      public virtual ICollection< Interested > Interested { get; set; }
      public virtual ICollection< AnnounceFormFieldsValues > AnnouncesFormFields { get; set; }

      public virtual ICollection< FeedBack > FeedBacks { get; set; }
   }
}