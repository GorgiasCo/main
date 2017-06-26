using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class RequestTypeValidator : AbstractValidator<RequestTypeDTO>
    {
        public RequestTypeValidator() {            
            RuleFor(requestType => requestType.RequestTypeName).NotEmpty().Length(1, 70).WithMessage("RequestTypeName is required");
            RuleFor(requestType => requestType.RequestTypeStatus).NotNull().WithMessage("RequestTypeStatus is required");
            RuleFor(requestType => requestType.RequestIsRestricted).NotNull().WithMessage("RequestIsRestricted is required");
        }
    }  
}   

