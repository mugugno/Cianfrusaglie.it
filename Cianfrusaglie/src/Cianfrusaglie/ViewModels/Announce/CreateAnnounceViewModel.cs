using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Attributes;
using Cianfrusaglie.Constants;
using Microsoft.AspNet.Http;

namespace Cianfrusaglie.ViewModels.Announce {



    public class CreateAnnounceViewModel {
        public bool vendita { get; set; }

        [Required,
         StringLength( DomainConstraints.AnnounceTitleMaxLenght,
             ErrorMessage = "Il titolo deve contenere almeno 3 caratteri",
             MinimumLength = DomainConstraints.AnnounceTitleMinLenght ), Display( Name = "Titolo" )]
        public string Title { get; set; }

        [Display( Name = "Descrizione" ), StringLength( DomainConstraints.AnnounceDescriptionMaxLenght )]
        public string Description { get; set; }

        [Display( Name = "Distanza" )]
        public int Range { get; set; }

        [Display( Name = "Price" )]
        public int Price { get; set; }

        public Dictionary< int, string > FormFieldDictionary { get; set; }

      [EnsureMinimumElements( 1, ErrorMessage = "Almeno una categoria richiesta!" )]
      public Dictionary< int, bool > CategoryDictionary { get; set; }

         [EnsureMinimumElements( 1, ErrorMessage = "Almeno una foto richiesta!" )]
         public ICollection< IFormFile > Photos { get; set; }
    }
}