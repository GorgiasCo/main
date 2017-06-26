using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class UserRoleValidator : AbstractValidator<UserRoleDTO>
    {
        public UserRoleValidator() {            
            RuleFor(userRole => userRole.UserRoleName).NotEmpty().Length(1, 70).WithMessage("UserRoleName is required");
            RuleFor(userRole => userRole.UserRoleStatus).NotNull().WithMessage("UserRoleStatus is required");
            RuleFor(userRole => userRole.UserRoleImage).Length(1, 150).WithMessage("UserRoleImage is required");
            RuleFor(userRole => userRole.UserRoleImage).Length(1, 150).WithMessage("UserRoleImage is required");
        }
    }  
}   

