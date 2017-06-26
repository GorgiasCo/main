using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(SubscriptionTypeValidator))]
    public class SubscriptionTypeDTO
    {
            public int SubscriptionTypeID{get; set;}
            public String SubscriptionTypeName{get; set;}
            public Decimal SubscriptionTypeFee{get; set;}
            public Boolean SubscriptionTypeStatus{get; set;}
            public String SubscriptionTypeImage{get; set;}
            public String SubscriptionTypeDescription{get; set;}
        
        
    }
}   

