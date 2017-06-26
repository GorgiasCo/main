using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class LowProfileModel
    {
        public ProfileSemiModel Profile { get; set; }
        public IEnumerable<SocialNetworkModel> SocialNetworks { get; set; }
    }
}