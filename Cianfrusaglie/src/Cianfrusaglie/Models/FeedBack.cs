using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.Models {
    public class FeedBack {
        public int Id { get; set; }

        [Range( DomainConstraints.FeedBackVoteMinRange, DomainConstraints.FeedBackVoteMaxRange )]
        public virtual int Vote { get; set; }

        [DataType( DataType.DateTime )]
        public virtual DateTime DateTime { get; set; }

        [MaxLength( DomainConstraints.FeedBackTextMaxLenght )]
        public virtual string Text { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public virtual User Receiver { get; set; }

        [ForeignKey( "Announce" )]
        public virtual int AnnounceId { get; set; }

        [Required]
        public virtual Announce Announce { get; set; }
    }
}