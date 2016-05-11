﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cianfrusaglie.Models;

namespace Cianfrusaglie.ViewModels.Announce
{
    public class CreateAnnounceViewModel
    {

        [Required]
        [StringLength(128, ErrorMessage = "Il titolo deve contenere almeno 3 caratteri", MinimumLength = 3)]
        [Display(Name = "Titolo")]
        public string Title { get; set; }

        [Display(Name = "Descrizione")]
        [StringLength(255)]
        public string Description { get; set; }

        
        [Display(Name = "Distanza")]
        public int Range { get; set; }

        public Dictionary<int, string> FormFieldDictionary { get; set; }

        public Dictionary<int, bool> CategoryDictionary { get; set; }

    }
}

