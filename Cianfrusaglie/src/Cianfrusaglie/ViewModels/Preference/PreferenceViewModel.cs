﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;
using Microsoft.AspNet.Http;

namespace Cianfrusaglie.ViewModels.Preference
{
    public class PreferenceViewModel
    {

        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }

        /*[Required,
         StringLength(DomainConstraints.UserPasswordMaxLengh,
             ErrorMessage = "La password deve contenere almeno 6 caratteri",
             MinimumLength = DomainConstraints.UserPasswordMinLengh), DataType(DataType.Password),
         Display(Name = "Nuova password")]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Conferma password"),
         Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }*/

        [Range((int)Constants.Genre.Unspecified, (int)Constants.Genre.Male)]
        public int Genre { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

       /* public IFormFile Photo { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }*/
        public Dictionary<int, bool> CategoryDictionary { get; set; }
    }
}
