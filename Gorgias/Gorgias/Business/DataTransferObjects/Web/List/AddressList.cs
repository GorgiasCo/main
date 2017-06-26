using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class AddressList
    {
        public AddressList()
        {
            Addresses = new List<AddressModelV2>();
        }
        public string CountryName { get; set; }
        public List<AddressModelV2> Addresses { get; set; }
    }
}