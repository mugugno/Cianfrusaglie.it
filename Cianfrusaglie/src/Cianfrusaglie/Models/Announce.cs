using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
    public class Announce {
        public int Id { get; set; }

        [Required]
        public virtual string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [DataType( DataType.DateTime )]
        public virtual DateTime PublishDate { get; set; }

        [DataType( DataType.DateTime )]
        public virtual DateTime? DeadLine { get; set; }

        [Required, MinLength( 3 ), MaxLength( 80 )]
        public virtual string Title { get; set; }

        [MaxLength( 255 )]
        public virtual string Description { get; set; }

        public virtual ICollection< ImageUrl > Images { get; set; }

        public virtual bool Closed { get; set; }

        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }

        [Range( 0, int.MaxValue )]
        public virtual int MeterRange { get; set; } // in kilometri

        /// <summary>
        ///     se = 0 è una donazione o un baratto
        /// </summary>
        [Range( 0, int.MaxValue )]
        public virtual int Price { get; set; }

        public virtual int PriceRange { get; set; }

        public virtual ICollection< AnnounceCategory > AnnounceCategories { get; set; }

        public virtual ICollection< AnnounceGat > AnnouncesGats { get; set; }
        public virtual ICollection< Interested > Interested { get; set; }
        public virtual ICollection< AnnounceFormFieldsValues > AnnouncesFormFields { get; set; }

        public virtual ICollection< FeedBack > FeedBacks { get; set; }
    }
}