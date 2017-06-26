using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class AddressValidator : AbstractValidator<AddressDTO>
    {
        public AddressValidator() {            
            RuleFor(address => address.AddressName).NotEmpty().Length(1, 70).WithMessage("AddressName is required");
            RuleFor(address => address.AddressStatus).NotNull().WithMessage("AddressStatus is required");
            RuleFor(address => address.AddressTel).NotEmpty().Length(1, 17).WithMessage("AddressTel is required");
            //RuleFor(address => address.AddressFax).Length(1, 17).WithMessage("AddressFax is required");
            //RuleFor(address => address.AddressZipCode).Length(1, 17).WithMessage("AddressZipCode is required");
            RuleFor(address => address.AddressAddress).NotEmpty().Length(1, 150).WithMessage("AddressAddress is required");
            RuleFor(address => address.AddressEmail).Length(1, 100).EmailAddress().WithMessage("AddressEmail is required");
            RuleFor(address => address.AddressImage).Length(1, 150).WithMessage("AddressImage is required");
            RuleFor(address => address.CityID).NotNull().WithMessage("City is required");
            RuleFor(address => address.ProfileID).NotNull().WithMessage("Profile is required");
            RuleFor(address => address.AddressTypeID).NotNull().WithMessage("AddressType is required");
        }
    }  
}   

