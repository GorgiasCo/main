using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileReadingValidator))]
    public class ProfileReadingDTO
    {
        public int ProfileReadingID { get; set; }
        public string ProfileReadingLanguageCode { get; set; }
        public DateTime ProfileReadingDatetime { get; set; }
        public int ProfileID { get; set; }

        public virtual ProfileDTO Profile { get; set; }

    }
}

