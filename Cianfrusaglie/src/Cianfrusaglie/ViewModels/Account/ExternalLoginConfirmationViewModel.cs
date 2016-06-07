using Cianfrusaglie.Constants;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.ViewModels.Account {
    public class ExternalLoginConfirmationViewModel {

        [Required,
         StringLength(DomainConstraints.UserUserNameMaxLenght,
             ErrorMessage = "Lo username deve contenere almeno 3 caratteri",
             MinimumLength = DomainConstraints.UserUserNameMinLenght), Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }

        [Range((int)Constants.Genre.Unspecified, (int)Constants.Genre.Male)]
        public int Genre { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public IFormFile Photo { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Dictionary<int, bool> CategoryDictionary { get; set; }
    }
}