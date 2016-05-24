using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.Models {
    public class FormField {
        public int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual FormFieldType Type { get; set; }

        public virtual ICollection< FieldDefaultValue > DefaultValues { get; set; }
        public virtual ICollection< CategoryFormField > CategoriesFormFields { get; set; }
        public virtual ICollection< AnnounceFormFieldsValues > AnnouncesFormFields { get; set; }
    }
}