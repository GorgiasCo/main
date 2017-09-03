using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(ContentRatingValidator))]
    public class ContentRatingDTO
    {
            public int ContentRatingID{get; set;}
            public String ContentRatingName{get; set;}
            public int ContentRatingAge{get; set;}
            public Boolean ContentRatingStatus{get; set;}
            public String ContentRatingImage{get; set;}
            public String ContentRatingDescription{get; set;}
            public int ContentRatingOrder{get; set;}
            public String ContentRatingLanguageCode{get; set;}
            public int? ContentRatingParentID{get; set;}
        
            public virtual ContentRatingDTO ContentRatingParent {get; set;}
        
    }
}   

