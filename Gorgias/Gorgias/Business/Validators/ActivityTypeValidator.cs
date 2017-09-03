using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ActivityTypeValidator : AbstractValidator<ActivityTypeDTO>
    {
        public ActivityTypeValidator() {            
            RuleFor(activityType => activityType.ActivityTypeName).NotEmpty().Length(1, 70).WithMessage("ActivityTypeName is required");
            RuleFor(activityType => activityType.ActivityTypeLanguageCode).NotEmpty().Length(1, 10).WithMessage("ActivityTypeLanguageCode is required");
            RuleFor(activityType => activityType.ActivityTypeStatus).NotNull().WithMessage("ActivityTypeStatus is required");
            RuleFor(activityType => activityType.ActivityTypeStatus).NotNull().WithMessage("ActivityTypeStatus is required");
        }
    }  
}   

