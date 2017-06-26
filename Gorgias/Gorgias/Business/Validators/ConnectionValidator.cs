using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class ConnectionValidator : AbstractValidator<ConnectionDTO>
    {
        public ConnectionValidator() {            
            RuleFor(connection => connection.RequestedProfileID).NotNull().WithMessage("RequestedProfileID is required");
            RuleFor(connection => connection.RequestTypeID).NotNull().WithMessage("RequestTypeID is required");
            RuleFor(connection => connection.ConnectStatus).NotNull().WithMessage("ConnectStatus is required");
            RuleFor(connection => connection.ConnectDateCreated).NotNull().WithMessage("ConnectDateCreated is required");
        }
    }  
}   

