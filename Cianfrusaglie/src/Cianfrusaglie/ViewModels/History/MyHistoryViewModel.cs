using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cianfrusaglie.Models;

namespace Cianfrusaglie.ViewModels.History
{
    public class MyHistoryViewModel
    {
        public List< Models.Announce > WonClosedAnnounces { get; set; }
        public List<Models.Announce> LostClosedAnnounces { get; set; }
        public List<Models.Announce> PublishedClosedAnnounces { get; set; }
    }
}
