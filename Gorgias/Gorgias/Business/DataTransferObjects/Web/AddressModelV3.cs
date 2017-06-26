using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class AddressModelV3
    {
        public string AddressName { get; set; }
        public bool AddressStatus { get; set; }
        public string AddressTel { get; set; }
        public string AddressFax { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressAddress { get; set; }
        public string AddressEmail { get; set; }
        public string AddressImage { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public int AddressTypeID { get; set; }
        public SqlGeography AddressLocation { get; set; }
        //public string getLat { get { return AddressLocation?.Latitude.ToString(); } }
        //public string getLng { get { return AddressLocation?.Longitude.ToString(); } }
        //public string getLat { get { return AddressLocation?.Lat.ToString(); } }
        //public string getLng { get { return AddressLocation?.Long.ToString(); } }
    }
}