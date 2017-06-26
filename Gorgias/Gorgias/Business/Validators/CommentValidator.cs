using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class CommentValidator : AbstractValidator<CommentDTO>
    {
        public CommentValidator() {            
            RuleFor(comment => comment.CommentNote).NotEmpty().Length(1, 16).WithMessage("CommentNote is required");
            RuleFor(comment => comment.CommentLike).NotNull().WithMessage("CommentLike is required");
            RuleFor(comment => comment.CommentDateTime).NotNull().WithMessage("CommentDateTime is required");
            RuleFor(comment => comment.CommentStatus).NotNull().WithMessage("CommentStatus is required");
            RuleFor(comment => comment.ProfileID).NotNull().WithMessage("Profile is required");
            RuleFor(comment => comment.ContentID).NotNull().WithMessage("Content is required");
        }
    }  
}   

