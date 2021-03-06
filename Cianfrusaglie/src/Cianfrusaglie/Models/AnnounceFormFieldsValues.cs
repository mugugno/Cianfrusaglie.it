using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.Models {
    public class AnnounceFormFieldsValues {
        [ForeignKey( "Announce" )]
        public int AnnounceId { get; set; }

        public virtual Announce Announce { get; set; }

        [ForeignKey( "FormField" )]
        public int FormFieldId { get; set; }

        public virtual FormField FormField { get; set; }

        [Required, MaxLength( DomainConstraints.AnnounceFormFieldsValuesValueMaxLength )]
        public string Value { get; set; }
    }
}