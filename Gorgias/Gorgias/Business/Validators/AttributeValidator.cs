using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class AttributeValidator : AbstractValidator<AttributeDTO>
    {
        public AttributeValidator() {            
            RuleFor(attribute => attribute.AttributeName).NotEmpty().Length(1, 70).WithMessage("AttributeName is required");
            RuleFor(attribute => attribute.AttributeCaption).NotEmpty().Length(1, 70).WithMessage("AttributeCaption is required");
            RuleFor(attribute => attribute.AttributeStatus).NotNull().WithMessage("AttributeStatus is required");
            RuleFor(attribute => attribute.AttributeMode).NotNull().WithMessage("AttributeMode is required");
            RuleFor(attribute => attribute.AttributeRule).Length(1, 200).WithMessage("AttributeRule is required");
            RuleFor(attribute => attribute.AttributeType).NotEmpty().Length(1, 50).WithMessage("AttributeType is required");
            RuleFor(attribute => attribute.AttributeImage).Length(1, 150).WithMessage("AttributeImage is required");
            RuleFor(attribute => attribute.AttributeImage).Length(1, 150).WithMessage("AttributeImage is required");
        }
    }  
}   

