using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(ProfileTypeValidator))]
    public class ProfileTypeDTO
    {
            public int ProfileTypeID{get; set;}
            public String ProfileTypeName{get; set;}
            public Boolean ProfileTypeStatus{get; set;}
            public String ProfileTypeImage{get; set;}
            public String ProfileTypeDescription{get; set;}
            public int ProfileTypeParentID{get; set;}
        
        
    }
}   

