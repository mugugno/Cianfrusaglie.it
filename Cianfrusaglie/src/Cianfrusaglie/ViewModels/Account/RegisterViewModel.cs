using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;
using Microsoft.AspNet.Http;

namespace Cianfrusaglie.ViewModels.Account {
    public class RegisterViewModel {
        [Required,
         StringLength( DomainConstraints.UserUserNameMaxLenght,
             ErrorMessage = "Lo username deve contenere almeno 3 caratteri",
             MinimumLength = DomainConstraints.UserUserNameMinLenght ), Display( Name = "Username" )]
        public string UserName { get; set; }

        [Required, EmailAddress, Display( Name = "Email" )]
        public string Email { get; set; }

        [Required,
         StringLength( DomainConstraints.UserPasswordMaxLengh,
             ErrorMessage = "La password deve contenere almeno 6 caratteri",
             MinimumLength = DomainConstraints.UserPasswordMinLengh ), DataType( DataType.Password ),
         Display( Name = "Password" )]
        public string Password { get; set; }

        [DataType( DataType.Password ), Display( Name = "Confirm password" ),
         Compare( "Password", ErrorMessage = "The password and confirmation password do not match." )]
        public string ConfirmPassword { get; set; }

        public IFormFile Photo { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}