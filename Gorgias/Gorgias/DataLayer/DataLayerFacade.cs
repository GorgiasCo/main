using Gorgias.DataLayer.Interface;
using Gorgias.DataLayer.Repository.SQL.Web;

namespace Gorgias.DataLayer
{       	     
    public static class DataLayerFacade
    {
        public static Interface.IContentTypeRepository ContentTypeRepository()
        {
            return new Repository.SQL.ContentTypeRepository();
        }
        public static Interface.IContentRatingRepository ContentRatingRepository()
        {
            return new Repository.SQL.ContentRatingRepository();
        }
        public static Interface.IActivityTypeRepository ActivityTypeRepository()
        {
            return new Repository.SQL.ActivityTypeRepository();
        }
        public static IFBActivityRepository FBActivityRepository()
        {
            return new Repository.SQL.FBActivityRepository();
        }
        public static IPaymentRepository PaymentRepository()
        {
            return new Repository.SQL.PaymentRepository();
        }
        public static IProfileCommissionRepository ProfileCommissionRepository()
        {
            return new Repository.SQL.ProfileCommissionRepository();
        }
        public static IProfileReportRepository ProfileReportRepository()
        {
            return new Repository.SQL.ProfileReportRepository();
        }
        public static IRevenueRepository RevenueRepository()
        {
            return new Repository.SQL.RevenueRepository();
        }
        public static IReportTypeRepository ReportTypeRepository()
        {
            return new Repository.SQL.ReportTypeRepository();
        }
        public static ICommentRepository CommentRepository()
        {
            return new Repository.SQL.CommentRepository();
        }
        public static IAlbumTypeRepository AlbumTypeRepository()
        {
            return new Repository.SQL.AlbumTypeRepository();
        }
        public static IProfileIndustryRepository ProfileIndustryRepository()
        {
            return new Repository.SQL.ProfileIndustryRepository();
        }
        public static WebRepository WebRepository()
        {
            return new WebRepository();
        }
        public static Interface.INewsletterRepository NewsletterRepository()
        {
            return new Repository.SQL.NewsletterRepository();
        }
        public static Interface.IProfileAttributeRepository ProfileAttributeRepository()
        {
            return new Repository.SQL.ProfileAttributeRepository();
        }
        public static Interface.IAddressRepository AddressRepository()
        {
            return new Repository.SQL.AddressRepository();
        }
        public static Interface.IAddressTypeRepository AddressTypeRepository()
        {
            return new Repository.SQL.AddressTypeRepository();
        }
        public static Interface.IAlbumRepository AlbumRepository()
        {
            return new Repository.SQL.AlbumRepository();
        }
        public static Interface.IAttributeRepository AttributeRepository()
        {
            return new Repository.SQL.AttributeRepository();
        }
        public static Interface.ICategoryRepository CategoryRepository()
        {
            return new Repository.SQL.CategoryRepository();
        }
        public static Interface.ICityRepository CityRepository()
        {
            return new Repository.SQL.CityRepository();
        }
        public static Interface.IConnectionRepository ConnectionRepository()
        {
            return new Repository.SQL.ConnectionRepository();
        }
        public static Interface.IContentRepository ContentRepository()
        {
            return new Repository.SQL.ContentRepository();
        }
        public static Interface.ICountryRepository CountryRepository()
        {
            return new Repository.SQL.CountryRepository();
        }
        public static Interface.IExternalLinkRepository ExternalLinkRepository()
        {
            return new Repository.SQL.ExternalLinkRepository();
        }
        public static Interface.IFeatureRepository FeatureRepository()
        {
            return new Repository.SQL.FeatureRepository();
        }
        public static Interface.IFeaturedSponsorRepository FeaturedSponsorRepository()
        {
            return new Repository.SQL.FeaturedSponsorRepository();
        }
        public static Interface.IIndustryRepository IndustryRepository()
        {
            return new Repository.SQL.IndustryRepository();
        }
        public static Interface.ILinkTypeRepository LinkTypeRepository()
        {
            return new Repository.SQL.LinkTypeRepository();
        }
        public static Interface.IMessageRepository MessageRepository()
        {
            return new Repository.SQL.MessageRepository();
        }
        public static Interface.IProfileRepository ProfileRepository()
        {
            return new Repository.SQL.ProfileRepository();
        }
        public static Interface.IProfileSocialNetworkRepository ProfileSocialNetworkRepository()
        {
            return new Repository.SQL.ProfileSocialNetworkRepository();
        }
        public static Interface.IProfileTagRepository ProfileTagRepository()
        {
            return new Repository.SQL.ProfileTagRepository();
        }
        public static Interface.IProfileTypeRepository ProfileTypeRepository()
        {
            return new Repository.SQL.ProfileTypeRepository();
        }
        public static Interface.IRequestTypeRepository RequestTypeRepository()
        {
            return new Repository.SQL.RequestTypeRepository();
        }
        public static Interface.ISocialNetworkRepository SocialNetworkRepository()
        {
            return new Repository.SQL.SocialNetworkRepository();
        }
        public static Interface.ISubscriptionTypeRepository SubscriptionTypeRepository()
        {
            return new Repository.SQL.SubscriptionTypeRepository();
        }
        public static Interface.ITagRepository TagRepository()
        {
            return new Repository.SQL.TagRepository();
        }
        public static Interface.IThemeRepository ThemeRepository()
        {
            return new Repository.SQL.ThemeRepository();
        }
        public static Interface.IUserRepository UserRepository()
        {
            return new Repository.SQL.UserRepository();
        }
        public static Interface.IUserProfileRepository UserProfileRepository()
        {
            return new Repository.SQL.UserProfileRepository();
        }
        public static Interface.IUserRoleRepository UserRoleRepository()
        {
            return new Repository.SQL.UserRoleRepository();
        }
    }
}   

