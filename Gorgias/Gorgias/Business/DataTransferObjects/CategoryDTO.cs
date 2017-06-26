using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(CategoryValidator))]
    public class CategoryDTO
    {
            public int CategoryID{get; set;}
            public String CategoryName{get; set;}
            public Boolean CategoryStatus{get; set;}
            public String CategoryImage{get; set;}
            public String CategoryDescription{get; set;}
            public int CategoryParentID{get; set;}
        
        
    }
}   

