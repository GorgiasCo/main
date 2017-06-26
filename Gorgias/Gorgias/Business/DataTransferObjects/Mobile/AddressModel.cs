using Microsoft.SqlServer.Types;

namespace Gorgias.Business.DataTransferObjects.Mobile
{
    public class AddressModel
    {
        public string AddressName { get; set; }
        public string AddressTel { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressAddress { get; set; }
        public string AddressTypeName { get; set; }
        public string AddressImage { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public SqlGeography AddressLocation { get; set; }
    }
}