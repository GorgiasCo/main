using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ContentValidator))]
    public class ContentDTO
    {
        public int ContentID { get; set; }
        public String ContentTitle { get; set; }
        public String ContentURL { get; set; }
        public int ContentType { get; set; }
        public Boolean ContentStatus { get; set; }
        public Boolean ContentIsDeleted { get; set; }
        public DateTime? ContentCreatedDate { get; set; }
        public int AlbumID { get; set; }

        public virtual AlbumDTO Album { get; set; }

    }
}

