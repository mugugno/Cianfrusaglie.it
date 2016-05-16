using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cianfrusaglie.Models;
using Microsoft.AspNet.Http;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.ViewModels.Announce
{
    public class CreateAnnounceViewModel
    {

        [Required]
        [StringLength(DomainConstraints.AnnounceTitleMaxLenght, ErrorMessage = "Il titolo deve contenere almeno 3 caratteri", MinimumLength = DomainConstraints.AnnounceTitleMinLenght)]
        [Display(Name = "Titolo")]
        public string Title { get; set; }

        [Display(Name = "Descrizione")]
        [StringLength(DomainConstraints.AnnounceDescriptionMaxLenght)]
        public string Description { get; set; }

        
        [Display(Name = "Distanza")]
        public int Range { get; set; }

        public Dictionary<int, string> FormFieldDictionary { get; set; }

        public Dictionary<int, bool> CategoryDictionary { get; set; }

        public ICollection<IFormFile> Photos { get; set; }

    }
}

