using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(RequestTypeValidator))]
    public class RequestTypeDTO
    {
            public int RequestTypeID{get; set;}
            public String RequestTypeName{get; set;}
            public Boolean RequestTypeStatus{get; set;}
            public Boolean RequestIsRestricted{get; set;}
        
        
    }
}   

