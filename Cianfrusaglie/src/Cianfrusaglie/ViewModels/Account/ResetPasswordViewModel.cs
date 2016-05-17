using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.ViewModels.Account {
    public class ResetPasswordViewModel {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required,
         StringLength( DomainConstraints.UserPasswordMaxLengh,
             ErrorMessage = "The {0} must be at least {2} characters long.",
             MinimumLength = DomainConstraints.UserPasswordMinLengh ), DataType( DataType.Password )]
        public string Password { get; set; }

        [DataType( DataType.Password ), Display( Name = "Confirm password" ),
         Compare( "Password", ErrorMessage = "The password and confirmation password do not match." )]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}