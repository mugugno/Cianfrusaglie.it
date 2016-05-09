using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Entity.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cianfrusaglie.Models {
   public class Message {
      public int Id { get; set; }

      [Required]
      public virtual string Text { get; set; }

      [DataType( DataType.DateTime )]
      public virtual DateTime DateTime { get; set; }

      [ForeignKey("Sender")]
      public int SenderId { get; set; }
      [Required]
      public virtual User Sender { get; set; }

      [ForeignKey( "Receiver" )]
      public int ReceiverId { get; set; }
      [Required]
      public virtual User Receiver { get; set; }
   }
}