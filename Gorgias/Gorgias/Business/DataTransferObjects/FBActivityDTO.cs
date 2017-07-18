using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(FBActivityValidator))]
    public class FBActivityDTO
    {
            public int FBActivityID{get; set;}
            public Decimal FBActivityCount{get; set;}
            public DateTime FBActivityDate{get; set;}
            public int FBActivityType{get; set;}
        
        
    }
}   

