using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(UserRoleValidator))]
    public class UserRoleDTO
    {
            public int UserRoleID{get; set;}
            public String UserRoleName{get; set;}
            public Boolean UserRoleStatus{get; set;}
            public String UserRoleImage{get; set;}
            public String UserRoleDescription{get; set;}
        
        
    }
}   

