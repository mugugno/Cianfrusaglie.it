using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Models
{
    public class UserFeedbackScore
    {
        public virtual int Id { get; set; }

        [Required, ForeignKey("User")]
        public virtual string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required, ForeignKey("FeedBack")]
        public virtual int FeedBackId { get; set; }

        public FeedBack FeedBack { get; set; }

        public virtual bool Useful { get; set; }
    }
}
