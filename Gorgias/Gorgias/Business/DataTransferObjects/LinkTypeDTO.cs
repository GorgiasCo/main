using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(LinkTypeValidator))]
    public class LinkTypeDTO
    {
            public int LinkTypeID{get; set;}
            public String LinkTypeName{get; set;}
            public Boolean LinkTypeStatus{get; set;}
            public String LinkTypeImage{get; set;}
            public String LinkTypeDescription{get; set;}
        
        
    }
}   

