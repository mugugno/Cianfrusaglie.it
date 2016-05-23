using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Models
{
    public class AnnounceChosen
    {
        public virtual int Id { get; set; }

        public virtual bool ReadNotification { get; set; }

        [DataType(DataType.DateTime)]
        public virtual DateTime ChosenDateTime { get; set; }

        [Required]
        public virtual string ChosenUserId { get; set; }

        public virtual User ChosenUser { get; set; }

        [Required]
        public virtual int AnnounceId { get; set; }

        public virtual Announce Announce { get; set; }
    }
}
