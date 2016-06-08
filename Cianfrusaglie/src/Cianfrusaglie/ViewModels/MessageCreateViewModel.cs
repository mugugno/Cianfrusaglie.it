using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;

namespace Cianfrusaglie.ViewModels {
    public class MessageCreateViewModel {
        [Required, MaxLength(DomainConstraints.MessageMaxLenght)]
        public string Text { get; set; }

        public string ReceiverId { get; set; }
    }
}