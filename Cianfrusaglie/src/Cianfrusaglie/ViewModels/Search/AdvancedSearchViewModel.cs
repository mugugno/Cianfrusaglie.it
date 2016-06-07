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

        public Tuple<int, int> KmRange { get; set; }

        public Tuple<int, int> PriceRange { get; set; }

        public Tuple<int, int> FeedbackRatingRange { get; set; }

        public bool ShowGifts { get; set; }

        public bool ShowOnSale { get; set; }
    }
}
