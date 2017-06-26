using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileValidator))]
    public class ProfileDTO
    {
        public int ProfileID { get; set; }
        public String ProfileFullname { get; set; }
        public string ProfileFullnameEnglish { get; set; }

        public Boolean ProfileIsPeople { get; set; }
        public Boolean ProfileIsDeleted { get; set; }
        public DateTime ProfileDateCreated { get; set; }
        public String ProfileDescription { get; set; }
        public int ProfileView { get; set; }
        public int ProfileLike { get; set; }
        public String ProfileURL { get; set; }
        public int ProfileCredit { get; set; }
        public String ProfileShortDescription { get; set; }
        public String ProfileImage { get; set; }
        public String ProfileEmail { get; set; }
        public Boolean ProfileStatus { get; set; }
        public Boolean ProfileIsConfirmed { get; set; }
        //public int IndustryID { get; set; }
        public int ProfileTypeID { get; set; }
        public int ThemeID { get; set; }
        public int SubscriptionTypeID { get; set; }

        //public virtual IndustryDTO Industry { get; set; }
        public virtual ProfileTypeDTO ProfileType { get; set; }
        public virtual ThemeDTO Theme { get; set; }
        public virtual SubscriptionTypeDTO SubscriptionType { get; set; }

    }
}

