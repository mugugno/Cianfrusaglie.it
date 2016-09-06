using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.ViewModels.Search
{
    public class MapSearchViewModel
    {
        public MapSearchViewModel()
        {
            this.mapOfPos = new Dictionary<string, List<Models.Announce>>();
        }
        public MapSearchViewModel(ICollection<Cianfrusaglie.Models.Announce> list)
        {
            this.mapOfPos = new Dictionary<string, List<Models.Announce>>();
            foreach(var announce in list)
            {
                List<Cianfrusaglie.Models.Announce> tempList;
                string pos = announce.Latitude.ToString() + "+" + announce.Longitude.ToString();
                if (mapOfPos.TryGetValue(pos, out tempList))
                {
                    tempList.Add(announce);
                }
                else
                {
                    tempList = new List<Cianfrusaglie.Models.Announce>();
                    tempList.Add(announce);
                    mapOfPos.Add(pos, tempList);
                }
            }
        }
        public Dictionary<string, List<Cianfrusaglie.Models.Announce>> mapOfPos { get; set; }
    }
}
