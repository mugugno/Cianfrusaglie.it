using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Models {
    public class UserCategoryPreferences {
        public virtual int Id { get; set; }
    
        [Required, ForeignKey( "User")]
        public virtual string UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
        
        [ForeignKey("Category")]
        public virtual int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; }
    }
}
