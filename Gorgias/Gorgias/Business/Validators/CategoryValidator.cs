using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator() {            
            RuleFor(category => category.CategoryName).NotEmpty().Length(1, 150).WithMessage("CategoryName is required");
            RuleFor(category => category.CategoryStatus).NotNull().WithMessage("CategoryStatus is required");
            RuleFor(category => category.CategoryImage).Length(1, 150).WithMessage("CategoryImage is required");
            RuleFor(category => category.CategoryImage).Length(1, 150).WithMessage("CategoryImage is required");
            RuleFor(category => category.CategoryImage).Length(1, 150).WithMessage("CategoryImage is required");
        }
    }  
}   

