﻿using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.ViewModels.Manage {
    public class SetPasswordViewModel {
        [Required,
         StringLength( DomainConstraints.UserPasswordMaxLengh,
             ErrorMessage = "The {0} must be at least {2} characters long.",
             MinimumLength = DomainConstraints.UserPasswordMinLengh ), DataType( DataType.Password ),
         Display( Name = "New password" )]
        public string NewPassword { get; set; }

        [DataType( DataType.Password ), Display( Name = "Confirm new password" ),
         Compare( "NewPassword", ErrorMessage = "The new password and confirmation password do not match." )]
        public string ConfirmPassword { get; set; }
    }
}