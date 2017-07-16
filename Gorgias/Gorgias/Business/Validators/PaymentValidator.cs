using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class PaymentValidator : AbstractValidator<PaymentDTO>
    {
        public PaymentValidator() {            
            RuleFor(payment => payment.PaymentDateCreated).NotNull().WithMessage("PaymentDateCreated is required");
            RuleFor(payment => payment.PaymentIsPaid).NotNull().WithMessage("PaymentIsPaid is required");
            RuleFor(payment => payment.PaymentAmount).NotNull().WithMessage("PaymentAmount is required");
            RuleFor(payment => payment.ProfileCommissionID).NotNull().WithMessage("ProfileCommission is required");
        }
    }  
}   

