﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class Announce {
      public int Id { get; set; }
      [Required]
      public virtual User Author { get; set; }
      [Required]
      public virtual DateTime PublishDate { get; set; }
      [Required, MinLength( 10 ), MaxLength( 255 )]
      public virtual string Description { get; set; }
      public virtual bool Closed { get; set; }
      [Required]
      public virtual string City { get; set; } //TODO tabella città...
      [Range( 0, int.MinValue )]
      public virtual int Range { get; set; } //TODO in metri?!
      //public virtual ICollection<Category> Categories { get; set; }

      //public virtual ICollection<AnnounceGat> Gats { get; set; }
      //public virtual ICollection<Interested> Interested { get; set; }
      //public virtual ICollection<FormFieldValues> FormFieldsValues { get; set; }

      //public virtual FeedBackOnAnnounce FeedBackOnAnnounce { get; set; }
   }
}