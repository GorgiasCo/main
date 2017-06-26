using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class MessageValidator : AbstractValidator<MessageDTO>
    {
        public MessageValidator() {            
            RuleFor(message => message.MessageNote).NotEmpty().Length(1, 16).WithMessage("MessageNote is required");
            RuleFor(message => message.MessageDateCreated).NotNull().WithMessage("MessageDateCreated is required");
            RuleFor(message => message.MessageSubject).NotEmpty().Length(1, 250).WithMessage("MessageSubject is required");
            RuleFor(message => message.MessageStatus).NotNull().WithMessage("MessageStatus is required");
            RuleFor(message => message.MessageIsDeleted).NotNull().WithMessage("MessageIsDeleted is required");
            RuleFor(message => message.ProfileID).NotNull().WithMessage("Profile is required");
        }
    }  
}   

