using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(UserValidator))]
    public class UserCustomDTO
    {
        public int UserID { get; set; }
        public String UserFullname { get; set; }
        public String UserEmail { get; set; }
        public Boolean UserStatus { get; set; }
        public Boolean UserIsBlocked { get; set; }
        public DateTime UserDateCreated { get; set; }
        public DateTime UserDateConfirmed { get; set; }
        public int? CountryID { get; set; }
        public virtual IEnumerable<UserProfileDTO> UserProfiles { get; set; }
    }
}

