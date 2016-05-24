using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.ViewModels.Manage {
    public class VerifyPhoneNumberViewModel {
        [Required]
        public string Code { get; set; }

        [Required, Phone, Display( Name = "Phone Number" )]
        public string PhoneNumber { get; set; }
    }
}