using System;
using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class AlbumUpdateMobileModel
    {
        public int AlbumID { get; set; }
        public string AlbumName { get; set; }
        public DateTime AlbumDateCreated { get; set; }
        public bool AlbumStatus { get; set; }
        public string AlbumCover { get; set; }
        public bool AlbumIsDeleted { get; set; }
        public int CategoryID { get; set; }
        public int ProfileID { get; set; }
        public int AlbumView { get; set; }
        public DateTime AlbumDateExpires { get; set; }
        public DateTime AlbumDatePublish { get; set; }
        public int AlbumAvailability { get; set; }
        public bool? AlbumHasComment { get; set; }

        //public virtual CategoryDTO Category { get; set; }
        //public virtual ProfileDTO Profile { get; set; }

        //V2
        public string AlbumReadingLanguageCode { get; set; }
        public int? AlbumRepostValue { get; set; }
        public int? AlbumRepostRequest { get; set; }
        public int? AlbumRepostAttempt { get; set; }
        public decimal? AlbumPrice { get; set; }
        public bool? AlbumIsTokenAvailable { get; set; }
        public int? AlbumPriceToken { get; set; }
        public int? ContentRatingID { get; set; }
        public int? AlbumParentID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ContentUpdateMobileModel> Contents { get; set; }

        public CategoryNewMobileModel Topic { get; set; }

        //public virtual ContentRatingDTO ContentRating { get; set; }
    }
}