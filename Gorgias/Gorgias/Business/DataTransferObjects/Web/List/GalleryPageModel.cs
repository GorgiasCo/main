using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class GalleryPageModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<AlbumModel> Albums { get; set; }
        public IEnumerable<SocialNetworkModel> SocialNetworks { get; set; }
        public ProfileSemiModel Profile { get; set; }
    }
}