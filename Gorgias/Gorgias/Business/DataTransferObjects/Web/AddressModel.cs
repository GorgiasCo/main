using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class AddressModel
    {
        public int ProfileID { get; set; }
        public string AddressName { get; set; }
        public string AddressTel { get; set; }
        public string AddressFax { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressAddress { get; set; }
        public string AddressEmail { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string AddressTypeName { get; set; }
        public int AddressTypeID { get; set; }
        public DbGeography AddressLocation { get; set; }

    }
}