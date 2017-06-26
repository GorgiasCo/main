using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class AddressTypeValidator : AbstractValidator<AddressTypeDTO>
    {
        public AddressTypeValidator() {            
            RuleFor(addressType => addressType.AddressTypeName).NotEmpty().Length(1, 150).WithMessage("AddressTypeName is required");
            RuleFor(addressType => addressType.AddressTypeImage).Length(1, 150).WithMessage("AddressTypeImage is required");
            RuleFor(addressType => addressType.AddressTypeStatus).NotNull().WithMessage("AddressTypeStatus is required");
        }
    }  
}   

