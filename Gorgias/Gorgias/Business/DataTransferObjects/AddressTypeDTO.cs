using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(AddressTypeValidator))]
    public class AddressTypeDTO
    {
        public int AddressTypeID { get; set; }
        public string AddressTypeName { get; set; }
        public string AddressTypeImage { get; set; }
        public bool AddressTypeStatus { get; set; }
    }
}

