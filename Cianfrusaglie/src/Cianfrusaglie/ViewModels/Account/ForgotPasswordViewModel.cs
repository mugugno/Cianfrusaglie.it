﻿using System.ComponentModel.DataAnnotations;

namespace Cianfrusaglie.ViewModels.Account {
    public class ForgotPasswordViewModel {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}