using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.ViewModels {
    public class LoginViewModel {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType( DataType.Password )]
        public string Password { get; set; }

        [Display( Name = "Ricordami?" )]
        public bool RememberMe { get; set; }
    }
}