using System;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
   public class Message {
      public int Id { get; set; }

      //[Required]
      public string Text { get; set; }

      public DateTime DateTime { get; set; }

      //[Required]
      public User Sender { get; set; }

      //[Required]
      public User Receiver { get; set; }
   }
}