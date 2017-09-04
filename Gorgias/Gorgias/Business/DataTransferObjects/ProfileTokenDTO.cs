using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(ProfileTokenValidator))]
    public class ProfileTokenDTO
    {
            public int ProfileTokenID{get; set;}
            public string ProfileTokenString{get; set;}
            public DateTime ProfileTokenRegistration{get; set;}
            public int ProfileID{get; set;}
        
            public virtual ProfileDTO Profile {get; set;}
        
    }
}   

