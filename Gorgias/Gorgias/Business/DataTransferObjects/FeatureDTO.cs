using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(FeatureValidator))]
    public class FeatureDTO
    {
            public int FeatureID{get; set;}
            public String FeatureTitle{get; set;}
            public DateTime FeatureDateCreated{get; set;}
            public DateTime FeatureDateExpired{get; set;}
            public Boolean FeatureStatus{get; set;}
            public Boolean FeatureIsDeleted{get; set;}
            public String FeatureImage{get; set;}
            public String FeatureDescription{get; set;}
            public int ProfileID{get; set;}
        
        
    }
}   

