using System.Collections.Generic;

namespace Gorgias.Business.DataTransferObjects.Web.List
{
    public class FeatureListModel
    {
        public ProfileModel Profile { get; set; }
        public IEnumerable<ExternalLinkModel> ExternalLinks { get; set; }
        public IEnumerable<SocialNetworkModel> SocialNetworks { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
        public IEnumerable<FeatureModel> CurrentFeature { get; set; }
        public IEnumerable<FeatureModel> ArchiveFeatures { get; set; }
        public IEnumerable<SponsoredFeatureModel> SponsoredFeature { get; set; }
    }
}