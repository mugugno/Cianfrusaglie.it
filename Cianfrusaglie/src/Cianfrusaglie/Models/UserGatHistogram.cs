using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Models
{
    public class UserGatHistogram
    {
        public virtual int Id { get; set; }

        [ForeignKey("Gat")]
        [Required]
        public virtual int GatId { get; set; }

        public virtual Gat Gat { get; set; }

        [Required]
        public virtual int Count { get; set; }

        [ForeignKey("User")]
        [Required]
        public virtual string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
