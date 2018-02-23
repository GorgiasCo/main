using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class AddressPageModelV2
    {
        public IEnumerable<AddressTypeModel> AddressTypes { get; set; }
        public IEnumerable<AddressModelV4> Addresses { get; set; }
    }
}