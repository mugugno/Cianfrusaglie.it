using System;
using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.Models {
    public class Message {
        public int Id { get; set; }

        [Required, MaxLength(DomainConstraints.MessageMaxLenght)]
        public virtual string Text { get; set; }

        [DataType( DataType.DateTime )]
        public virtual DateTime DateTime { get; set; }

        [Required]
        public virtual User Sender { get; set; }

        [Required]
        public virtual User Receiver { get; set; }
    }
}