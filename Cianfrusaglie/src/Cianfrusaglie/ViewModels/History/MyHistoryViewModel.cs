using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Models;

namespace Cianfrusaglie.ViewModels.History {
    public class MyHistoryViewModel {
        public ICollection<Models.Announce> WonClosedAnnounces { get; set; }
        public ICollection<Models.Announce> LostClosedAnnounces { get; set; }
        public ICollection<Models.Announce> PublishedClosedAnnounces { get; set; }
    }
}
