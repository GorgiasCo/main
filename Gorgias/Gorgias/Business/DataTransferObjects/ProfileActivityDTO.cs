using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileActivityValidator))]
    public class ProfileActivityDTO
    {
        public int ProfileID { get; set; }
        public int AlbumID { get; set; }
        public int ActivityTypeID { get; set; }
        public int ProfileActivityCount { get; set; }
        public DateTime ProfileActivityDateTime { get; set; }

        public virtual AlbumDTO Album { get; set; }
        public virtual ProfileDTO Profile { get; set; }
        public virtual ActivityTypeDTO ActivityType { get; set; }
    }
}

