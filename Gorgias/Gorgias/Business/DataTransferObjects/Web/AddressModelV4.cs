using Microsoft.SqlServer.Types;

namespace Gorgias.Business.DataTransferObjects.Web
{
    public class AddressModelV4
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
        public int AddressID { get; set; }
    }
}