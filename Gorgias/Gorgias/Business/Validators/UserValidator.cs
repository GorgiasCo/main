using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator() {            
            RuleFor(user => user.UserFullname).NotEmpty().Length(1, 150).WithMessage("UserFullname is required");
            RuleFor(user => user.UserEmail).NotEmpty().Length(1, 250).EmailAddress().WithMessage("UserEmail is required");
            RuleFor(user => user.UserStatus).NotNull().WithMessage("UserStatus is required");
            RuleFor(user => user.UserIsBlocked).NotNull().WithMessage("UserIsBlocked is required");
        }
    }  
}   

