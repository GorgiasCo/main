using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class ProfileSliderModel
    {
        public ProfileModel Profile { get; set; }
        public IEnumerable<ConnectionModel> Connections { get; set; }
        public IEnumerable<SocialNetworkModel> SocialNetworks { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}