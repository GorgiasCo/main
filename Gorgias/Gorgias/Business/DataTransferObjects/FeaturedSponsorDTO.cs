using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(FeaturedSponsorValidator))]
    public class FeaturedSponsorDTO
    {
        public int FeatureID { get; set; }
        public int ProfileID { get; set; }
        public int FeaturedSponsorMode { get; set; }
        public int FeaturedRole { get; set; }
        public virtual FeatureDTO Feature {get; set;}
        public virtual ProfileDTO Profile { get; set; }

    }
}

