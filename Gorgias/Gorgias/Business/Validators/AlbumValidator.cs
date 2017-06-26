using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{   
        
    public class AlbumValidator : AbstractValidator<AlbumDTO>
    {
        public AlbumValidator() {            
            RuleFor(album => album.AlbumName).NotEmpty().Length(1, 500).WithMessage("AlbumName is required");
            RuleFor(album => album.AlbumDateCreated).NotNull().WithMessage("AlbumDateCreated is required");
            RuleFor(album => album.AlbumStatus).NotNull().WithMessage("AlbumStatus is required");
            RuleFor(album => album.AlbumCover).Length(1, 250).WithMessage("AlbumCover is required");
            RuleFor(album => album.AlbumIsDeleted).NotNull().WithMessage("AlbumIsDeleted is required");
            RuleFor(album => album.CategoryID).NotNull().WithMessage("Category is required");
            RuleFor(album => album.ProfileID).NotNull().WithMessage("Profile is required");
        }
    }  
}   

