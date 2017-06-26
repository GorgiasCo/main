using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(ThemeValidator))]
    public class ThemeDTO
    {
            public int ThemeID{get; set;}
            public String ThemeName{get; set;}
            public String ThemeClassCode{get; set;}
            public Boolean ThemeStatus{get; set;}
        
        
    }
}   

