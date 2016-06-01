using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public virtual bool Read { get; set; }

        public virtual MessageTypeNotification TypeNotification { get; set; }

        [Required, ForeignKey("User")]
        public virtual string UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
        
    }
}
