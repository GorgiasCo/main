using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(QuoteValidator))]
    public class QuoteDTO
    {
            public int QuoteID{get; set;}
            public String QuoteName{get; set;}
            public Boolean QuoteStatus{get; set;}
            public String QuoteLanguageCode{get; set;}
            public int QuoteProbability{get; set;}
            public int CategoryID{get; set;}
        
            public virtual CategoryDTO Category {get; set;}
        
    }
}   

