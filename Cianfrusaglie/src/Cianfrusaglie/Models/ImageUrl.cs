using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;
using Microsoft.Data.Entity.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cianfrusaglie.Models {
    public class ImageUrl {
        public int Id { get; set; }

        [Required, MaxLength( DomainConstraints.ImageUrlUrlMaxLenght )]
        public virtual string Url { get; set; }

        [ForeignKey("Announce")]
        public virtual int AnnounceId { get; set; }

        [Required]
        public virtual Announce Announce { get; set; }
    }
}