using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.Models {
    public class ImageUrl {
        public int Id { get; set; }

        [Required, MaxLength( DomainConstraints.ImageUrlUrlMaxLenght )]
        public virtual string Url { get; set; }

        [Required]
        public virtual int AnnounceId { get; set; }

        public virtual Announce Announce { get; set; }
    }
}