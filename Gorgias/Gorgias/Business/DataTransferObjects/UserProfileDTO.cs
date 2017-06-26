using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(UserProfileValidator))]
    public class UserProfileDTO
    {
        public int ProfileID { get; set; }
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int? CountryID { get; set; }

        public virtual UserRoleDTO UserRole { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual ProfileDTO Profile { get; set; }
        public virtual CountryDTO Country { get; set; }
    }
}

