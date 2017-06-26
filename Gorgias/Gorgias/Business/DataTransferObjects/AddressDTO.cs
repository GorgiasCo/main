using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
using System.Data.Entity.Spatial;

namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(AddressValidator))]
    public class AddressDTO
    {        
        public int AddressID { get; set; }
        public string AddressName { get; set; }
        public bool AddressStatus { get; set; }
        public string AddressTel { get; set; }
        public string AddressFax { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressAddress { get; set; }
        public string AddressEmail { get; set; }
        public string AddressImage { get; set; }
        public int CityID { get; set; }
        public int ProfileID { get; set; }
        public int AddressTypeID { get; set; }
        public DbGeography AddressLocation { get; set; }
        public string AddressStringLocation { get; set; }
        public virtual CityDTO City { get; set; }
        public virtual ProfileDTO Profile { get; set; }
        public virtual AddressTypeDTO AddressType { get; set; }

        public string getLat { get { return AddressLocation?.Latitude.ToString(); } }
        public string getLng { get { return AddressLocation?.Longitude.ToString(); } }        

    }
}

