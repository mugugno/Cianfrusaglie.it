using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Models;

namespace Cianfrusaglie.ViewModels {
   public class MessageCreateViewModel {
       [Required]
       public string Text { get; set; }

       public string ReceiverId { get; set; }
   }
}