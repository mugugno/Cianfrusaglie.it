using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.Models {
    public class Gat {
        public int Id { get; set; }

        [Required, MinLength( DomainConstraints.GatTextMinLenght ), MaxLength( DomainConstraints.GatTextMaxLenght )]
        public virtual string Text { get; set; }

        public virtual ICollection< AnnounceGat > AnnouncesGats { get; set; }
    }
}