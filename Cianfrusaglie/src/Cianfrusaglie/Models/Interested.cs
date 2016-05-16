using System;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.Models {
    public class Interested {
        public int Id { get; set; }

        [DataType( DataType.DateTime )]
        public virtual DateTime DateTime { get; set; }

        public virtual int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Announce Announce { get; set; }

        /// <summary>
        ///     Choosen = true se ChooseDate != null, altrimenti Choosen = false
        ///     posso dare feedback a uno che è stato scelto (ChooseDate != null)
        ///     quello con ChooseDate > di tutti è il tizio a cui è stato assegnato l'oggetto...
        /// </summary>
        public virtual DateTime? ChooseDate { get; set; }
    }
}