using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class FreshProfiles
    {
        public IEnumerable<ProfileItemModel> Webs { get; set; }
        public IEnumerable<ProfileItemModel> Apps { get; set; }
        public IEnumerable<ProfileItemModel> Sliders { get; set; }
    }
}