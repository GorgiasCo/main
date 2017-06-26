using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(AlbumTypeValidator))]
    public class AlbumTypeDTO
    {
        public int AlbumTypeID { get; set; }
        public String AlbumTypeName { get; set; }
        public Boolean AlbumTypeStatus { get; set; }
        public int AlbumTypeLimitation { get; set; }
    }
}

