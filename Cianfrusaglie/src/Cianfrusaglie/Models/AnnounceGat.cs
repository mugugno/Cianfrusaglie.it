using System.ComponentModel.DataAnnotations.Schema;

namespace Cianfrusaglie.Models {
    public class AnnounceGat {
        [ForeignKey( "Announce" )]
        public int AnnounceId { get; set; }

        public virtual Announce Announce { get; set; }

        [ForeignKey( "Gat" )]
        public int GatId { get; set; }

        public virtual Gat Gat { get; set; }
    }
}