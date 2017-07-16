using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(ProfileCommissionValidator))]
    public class ProfileCommissionDTO
    {
            public int ProfileCommissionID{get; set;}
            public double ProfileCommissionRate{get; set;}
            public DateTime ProfileCommissionDateCreated{get; set;}
            public Boolean ProfileCommissionStatus{get; set;}
            public int ProfileID{get; set;}
            public int UserID{get; set;}
            public int UserRoleID{get; set;}
        
            public virtual ProfileDTO Profile {get; set;}
            public virtual UserDTO User {get; set;}
            public virtual UserRoleDTO UserRole {get; set;}
        
    }
}   

