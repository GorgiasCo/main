using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(SocialNetworkValidator))]
    public class SocialNetworkDTO
    {
            public int SocialNetworkID{get; set;}
            public String SocialNetworkName{get; set;}
            public Boolean SocialNetworkStatus{get; set;}
            public String SocialNetworkURL{get; set;}
            public String SocialNetworkImage{get; set;}
            public String SocialNetworkDescription{get; set;}
        
        
    }
}   

