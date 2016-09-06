using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Models
{
    public class Region
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Latitudine { get; set; }
        [Required]
        public double Longitudine { get; set; }
    }
}
