using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class AddressModelV2
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
        public virtual CityDTO City { get; set; }
        public virtual ProfileDTO Profile { get; set; }
        public virtual AddressTypeDTO AddressType { get; set; }

        public string getLat { get { return AddressLocation?.Latitude.ToString(); } }
        public string getLng { get { return AddressLocation?.Longitude.ToString(); } }
    }
}