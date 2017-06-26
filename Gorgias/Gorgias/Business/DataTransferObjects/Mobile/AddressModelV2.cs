using System.Data.Entity.Spatial;

namespace Gorgias.Business.DataTransferObjects.Mobile
{
    public class AddressModelV2
    {
        public string AddressName { get; set; }
        public string AddressTel { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressAddress { get; set; }
        public string AddressTypeName { get; set; }
        public string AddressImage { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public DbGeography AddressLocation { get; set; }

        public string getLat { get { return AddressLocation?.Latitude.ToString(); } }
        public string getLng { get { return AddressLocation?.Longitude.ToString(); } }
    }
}