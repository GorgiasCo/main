using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(CountryValidator))]
    public class CountryDTO
    {
            public int CountryID{get; set;}
            public String CountryName{get; set;}
            public String CountryShortName{get; set;}
            public Boolean CountryStatus{get; set;}
            public String CountryPhoneCode{get; set;}
            public String CountryImage{get; set;}
            public String CountryDescription{get; set;}
        
        
    }
}   

