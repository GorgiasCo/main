using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(IndustryValidator))]
    public class IndustryDTO
    {
            public int IndustryID{get; set;}
            public String IndustryName{get; set;}
            public Boolean IndustryStatus{get; set;}
            public int IndustryParentID{get; set;}
            public String IndustryImage{get; set;}
            public String IndustryDescription{get; set;}
        
        
    }
}   

