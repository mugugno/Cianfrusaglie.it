using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Models;

namespace Cianfrusaglie.ViewModels.InterestedAnnounce
{
    public class InterestedAnnounceViewModel
    {
        [Required]
        public Models.Announce  Announce { get; set; }

        [Required]
        public List<User> InterestedUsers { get; set; }
    }
}
