using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.ViewModels.Announce
{
    public class EditAnnounceViewModel : CreateAnnounceViewModel
    {
        public int AnnounceId { get; set; }
        public string AuthorId { get; set; }

    }
}
