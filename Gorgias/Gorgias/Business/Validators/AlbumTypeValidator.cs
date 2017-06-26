using Gorgias.Business.DataTransferObjects;
using FluentValidation;
namespace Gorgias.Business.Validators
{
    public class AlbumTypeValidator : AbstractValidator<AlbumTypeDTO>
    {
        public AlbumTypeValidator()
        {
            //RuleFor(albumType => albumType.AlbumTypeName).NotEmpty()..WithMessage("AlbumID must be uniqe");
            RuleFor(albumType => albumType.AlbumTypeName).NotEmpty().Length(1, 150).WithMessage("AlbumTypeName is required");
            RuleFor(albumType => albumType.AlbumTypeStatus).NotNull().WithMessage("AlbumTypeStatus is required");
            RuleFor(albumType => albumType.AlbumTypeLimitation).NotNull().WithMessage("AlbumTypeLimitation is required");
        }
    }
}

