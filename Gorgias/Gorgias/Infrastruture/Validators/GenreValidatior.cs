using FluentValidation;
using Gorgias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Infrastruture.Validators
{
    public class GenreValidator : AbstractValidator<Genre>
    {
        public GenreValidator() {
            RuleFor(genre => genre.Name).NotEmpty().Length(1, 100).WithMessage("Select a name");
            //RuleFor(genre => genre.ID).NotNull().WithMessage("Select a name");
            //RuleFor(genre => genre.Status).NotNull().WithMessage("Select a name");
            //RuleFor(genre => genre.Time).NotNull().WithMessage("Select a name");
        }

    }
}