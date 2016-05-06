using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cianfrusaglie.Models {
   public class FeedBack {
      [Range( 0, 5 )]
      public virtual int Vote { get; set; }

      [DataType( DataType.DateTime )]
      public virtual DateTime DateTime { get; set; }

      [MaxLength( 99 )]
      public virtual string Text { get; set; }

      [ForeignKey( "Sender" )]
      public virtual int SenderId { get; set; }

      [Required]
      public virtual User Sender { get; set; }

      [ForeignKey( "Receiver" )]
      public virtual int ReceiverId { get; set; }

      [Required]
      public virtual User Receiver { get; set; }

      [ForeignKey( "Announce" )]
      public virtual int AnnounceId { get; set; }

      [Required]
      public virtual Announce Announce { get; set; }
   }
}