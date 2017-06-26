using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileIndustryValidator))]
    public class ProfileIndustryDTO
    {
        public int IndustryID { get; set; }
        public int ProfileID { get; set; }

        public virtual ProfileDTO Profile { get; set; }
        public virtual IndustryDTO Industry { get; set; }

    }
}

