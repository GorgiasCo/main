using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class AddressPageModel
    {
        public IEnumerable<AddressTypeModel> AddressTypes { get; set; }
        public IEnumerable<AddressModelV3> Addresses { get; set; }
    }
}