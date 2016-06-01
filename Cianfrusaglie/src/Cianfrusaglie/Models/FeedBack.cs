using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.Models {
    public class FeedBack {
        public int Id { get; set; }

        [Range( DomainConstraints.FeedBackVoteMinRange, DomainConstraints.FeedBackVoteMaxRange,ErrorMessage = "Dev'essere un numero compreso tra 0 e 5")]
        public virtual int Vote { get; set; }

        [DataType( DataType.DateTime )]
        public virtual DateTime DateTime { get; set; }

        [MaxLength( DomainConstraints.FeedBackTextMaxLenght )]
        public virtual string Text { get; set; }

        [Required, ForeignKey("User")]
        public virtual string AuthorId { get; set; }
        
        public virtual User Author { get; set; }

        [Required, ForeignKey( "User")]
        public virtual string ReceiverId { get; set; }
        
        public virtual User Receiver { get; set; }

        [Required, ForeignKey( "Announce" )]
        public virtual int AnnounceId { get; set; }
        
        public virtual Announce Announce { get; set; }

        public virtual ICollection< UserFeedbackScore > UserFeedbackScores { get; set; }

        public virtual int Usefulness { get; set; }
    }
}