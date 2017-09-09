using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(AlbumValidator))]
    public class AlbumDTO
    {
        public int AlbumID { get; set; }
        public String AlbumName { get; set; }
        public DateTime AlbumDateCreated { get; set; }
        public Boolean AlbumStatus { get; set; }
        public String AlbumCover { get; set; }
        public Boolean AlbumIsDeleted { get; set; }
        public int CategoryID { get; set; }
        public int ProfileID { get; set; }
        public int AlbumView { get; set; }
        public DateTime AlbumDateExpires { get; set; }
        public DateTime AlbumDatePublish { get; set; } 
        public int AlbumAvailability { get; set; }
        public bool? AlbumHasComment { get; set; }

        public virtual CategoryDTO Category { get; set; }
        public virtual ProfileDTO Profile { get; set; }

        //V2
        public string AlbumReadingLanguageCode { get; set; }
        public int? AlbumRepostValue { get; set; }
        public int? AlbumRepostRequest { get; set; }
        public int? AlbumRepostAttempt { get; set; }
        public decimal? AlbumPrice { get; set; }
        public bool? AlbumIsTokenAvailable { get; set; }
        public int? AlbumPriceToken { get; set; }
        public int? ContentRatingID { get; set; }

        public virtual ContentRatingDTO ContentRating { get; set; }
    }
}

