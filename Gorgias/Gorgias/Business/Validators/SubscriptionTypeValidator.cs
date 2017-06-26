using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class SubscriptionTypeValidator : AbstractValidator<SubscriptionTypeDTO>
    {
        public SubscriptionTypeValidator() {            
            RuleFor(subscriptionType => subscriptionType.SubscriptionTypeName).NotEmpty().Length(1, 100).WithMessage("SubscriptionTypeName is required");
            RuleFor(subscriptionType => subscriptionType.SubscriptionTypeFee).NotNull().WithMessage("SubscriptionTypeFee is required");
            RuleFor(subscriptionType => subscriptionType.SubscriptionTypeStatus).NotNull().WithMessage("SubscriptionTypeStatus is required");
            RuleFor(subscriptionType => subscriptionType.SubscriptionTypeImage).Length(1, 150).WithMessage("SubscriptionTypeImage is required");
            RuleFor(subscriptionType => subscriptionType.SubscriptionTypeImage).Length(1, 150).WithMessage("SubscriptionTypeImage is required");
        }
    }  
}   

