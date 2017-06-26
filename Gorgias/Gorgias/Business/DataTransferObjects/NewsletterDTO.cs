using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(NewsletterValidator))]
    public class NewsletterDTO
    {
            public int NewsletterID{get; set;}
            public String NewsletterName{get; set;}
            public String NewsletterNote{get; set;}
            public Boolean NewsletterStatus{get; set;}
        
        
    }
}   

