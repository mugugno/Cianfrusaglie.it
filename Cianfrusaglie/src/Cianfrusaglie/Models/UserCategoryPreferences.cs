using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Models
{
    public class UserCategoryPreferences
    {
        public virtual int Id { get; set; }
    
        [ForeignKey("User")]
        [Required]
        public virtual string UserId { get; set; }

        public virtual User User { get; set; }
        
        [ForeignKey("Category")]
        [Required]
        public virtual int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
