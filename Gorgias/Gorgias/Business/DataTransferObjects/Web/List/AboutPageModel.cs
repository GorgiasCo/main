using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class AboutPageModel
    {
        public ProfileSemiModel Profile { get; set; }
        public IEnumerable<AboutModel> Abouts { get; set; }
        public IEnumerable<AlbumModel> Albums { get; set; }
        public IEnumerable<SocialNetworkModel> SocialNetworks { get; set; }
    }
}