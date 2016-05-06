﻿using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class ImageUrl {
      public int Id { get; set; }
      [Required]
      public virtual string Url { get; set; }
      [Required]
      public virtual Announce Announce { get; set; }
   }
}