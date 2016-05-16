using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.Constants
{
    public static class DomainConstraints {
        //Constraints per l'Annuncio
        public const int AnnounceTitleMinLenght = 3;
        public const int AnnounceTitleMaxLenght = 80;
        public const int AnnounceDescriptionMaxLenght = 255;
        public const int AnnounceMeterRangeMinLenght = 0;
        public const int AnnounceMeterRangeMaxLenght = int.MaxValue;
        public const int AnnouncePriceMinLenght = 0;
        public const int AnnouncePriceMaxLenght = int.MaxValue;



    }
}
