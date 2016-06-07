using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.ViewModels.Search
{
    public class AdvancedSearchViewModel
    {
        public string Title { get; set; }

        public bool OrderByDate { get; set; }

        public bool OrderByPrice { get; set; }

        public bool OrderByDistance { get; set; }

        public int KmRangeMin { get; set; }
        public int? KmRangeMax { get; set; }

        public int PriceRangeMin { get; set; }
        public int? PriceRangeMax { get; set; }

        public int FeedbackRangeMin { get; set; }
        public int? FeedbackRangeMax { get; set; }

        public bool ShowGifts { get; set; }

        public bool ShowOnSale { get; set; }
    }
}
