using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileSocialNetworkValidator))]
    public class ProfileSocialNetworkDTO
    {
        public int SocialNetworkID { get; set; }
        public int ProfileID { get; set; }
        public String ProfileSocialNetworkURL { get; set; }
        public virtual SocialNetworkDTO SocialNetwork { get; set; }
        public virtual ProfileDTO Profile { get; set; }

    }
}

