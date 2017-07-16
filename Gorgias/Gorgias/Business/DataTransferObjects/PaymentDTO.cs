using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{   

    [Validator(typeof(PaymentValidator))]
    public class PaymentDTO
    {
            public int PaymentID{get; set;}
            public DateTime PaymentDateCreated{get; set;}
            public DateTime PaymentDatePaid{get; set;}
            public Boolean PaymentIsPaid{get; set;}
            public double PaymentAmount{get; set;}
            public String PaymentNote{get; set;}
            public int ProfileCommissionID{get; set;}
        
            public virtual ProfileCommissionDTO ProfileCommission {get; set;}
        
    }
}   

