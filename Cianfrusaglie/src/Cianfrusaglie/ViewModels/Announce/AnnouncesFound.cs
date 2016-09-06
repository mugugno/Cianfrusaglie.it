using Cianfrusaglie.Constants;
using Cianfrusaglie.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cianfrusaglie.ViewModels.Announce
{
    public class AnnouncesFound 
    {
        public AnnouncesFound() { }
        public AnnouncesFound(Cianfrusaglie.Models.Announce announce){
            this.Id = announce.Id;
            this.Author = announce.Author;
            this.AuthorId = announce.AuthorId;
            this.Title = announce.Title;
            this.Description = announce.Description;
            this.Price = announce.Price;
            this.MeterRange = announce.MeterRange;
            this.Latitude = announce.Latitude;
            this.Longitude = announce.Longitude;
            this.AnnounceCategories = announce.AnnounceCategories;
            this.Images = announce.Images;
            this.isInRange = false;
        }
        public int Id { get; set; }

        [Required]
        public virtual string AuthorId { get; set; }

        public virtual User Author { get; set; }


        [Required, MinLength(DomainConstraints.AnnounceTitleMinLenght),
         MaxLength(DomainConstraints.AnnounceTitleMaxLenght)]
        public virtual string Title { get; set; }

        [MaxLength(DomainConstraints.AnnounceDescriptionMaxLenght)]
        public virtual string Description { get; set; }

        public virtual ICollection<ImageUrl> Images { get; set; }
        

        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }

        [Range(DomainConstraints.AnnounceMeterRangeMinLenght, DomainConstraints.AnnounceMeterRangeMaxLenght)]
        public virtual int MeterRange { get; set; } // in kilometri

        /// <summary>
        ///     se = 0 è una donazione o un baratto
        /// </summary>
        [Range(DomainConstraints.AnnouncePriceMinLenght, DomainConstraints.AnnouncePriceMaxLenght)]
        public virtual int Price { get; set; }

        public virtual int PriceRange { get; set; }

        public virtual ICollection<AnnounceCategory> AnnounceCategories { get; set; }

        public virtual ICollection<AnnounceGat> AnnouncesGats { get; set; }
        public virtual ICollection<Interested> Interested { get; set; }
        public virtual ICollection<AnnounceFormFieldsValues> AnnouncesFormFields { get; set; }

        public virtual ICollection<FeedBack> FeedBacks { get; set; }

        public virtual ICollection<AnnounceChosen> ChosenUsers { get; set; }
        [Required]
        public virtual Region Region { get; set; }
        public bool isInRange { get; set; }
    }
}
