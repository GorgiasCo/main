using AutoMapper;
using Gorgias.Business.DataTransferObjects;
using Gorgias.Business.DataTransferObjects.Email;
using Gorgias.Business.DataTransferObjects.Web;
using Gorgias.Business.DataTransferObjects.Web.List;
using Gorgias.BusinessLayer.Facades;
using Gorgias.DataLayer.Repository.SQL.Web;
using Gorgias.Infrastruture.Core.Email;
using Gorgias.Infrastruture.Core.Upload.Provider;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace Gorgias.Business.Facades.Web
{
    public class WebFacade
    {
        public bool SendRegistrationEmail(int ProfileID)
        {
            ProfileDTO profile = new BusinessLayer.Facades.ProfileFacade().GetProfile(ProfileID);
            NewsletterDTO newsletter = new BusinessLayer.Facades.NewsletterFacade().GetNewsletter("Registration-Body");

            if (newsletter.NewsletterStatus)
            {
                Dictionary<string, string> values = new Dictionary<string, string>();

                StringBuilder stringBody = new StringBuilder();

                stringBody.AppendFormat(newsletter.NewsletterNote, profile.ProfileFullname, profile.ProfileEmail);
                values.Add("##Body##", stringBody.ToString());

                EmailDTO email = new EmailDTO();
                email.TO = "yaser2us@gmail.com";// profile.ProfileEmail;
                email.Subject = "Done ;)";

                return EmailHelper.Send(email, values);
            }
            return false;
        }

        public IEnumerable<DataTransferObjects.Mobile.AlbumAvailabilityModel> AlbumAvailability(int ProfileID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int32;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<DataTransferObjects.Mobile.AlbumAvailabilityModel>("[dbo].[getAblumAvailability]", param);
            return (IEnumerable<DataTransferObjects.Mobile.AlbumAvailabilityModel>)r[0];
        }

        public bool GenerateQRCode(int ProfileID)
        {
            Profile profile = DataLayer.DataLayerFacade.ProfileRepository().GetProfile(ProfileID);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(ConfigurationManager.AppSettings["WebURL"] + profile.ProfileURL, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            UploadQRProvider.UploadQR(qrCodeImage, "qr-web-" + ProfileID + ".png");
            //Test Only ;)
            //SendRegistrationEmail(ProfileID);
            return true;
        }

        public bool SendContact(int ProfileID, int RequestedProfileID, string Subject, string Note)
        {
            Message result = DataLayer.DataLayerFacade.MessageRepository().Insert(Note, DateTime.Now, Subject, false, false, ProfileID, RequestedProfileID);
            if (result != null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public string Subscribe(int ProfileID, int RequestedProfileID, bool Status)
        {
            var profile = DataLayer.DataLayerFacade.ProfileRepository().GetProfile(ProfileID);
            var requestedProfile = DataLayer.DataLayerFacade.ProfileRepository().GetProfile(RequestedProfileID);

            if (!Status)
            {
                var resultDelete = DataLayer.DataLayerFacade.ConnectionRepository().Delete(ProfileID, RequestedProfileID);
                DataLayer.DataLayerFacade.WebRepository().updateProfileLike(RequestedProfileID);
                return "Subscribe";
            }

            Connection result;
            if (profile.ProfileIsConfirmed && profile.ProfileIsPeople && !requestedProfile.ProfileIsPeople && !requestedProfile.ProfileIsConfirmed)
            {
                result = DataLayer.DataLayerFacade.ConnectionRepository().Insert(ProfileID, RequestedProfileID, 1, true, DateTime.Now);
                if (result.ProfileID != 0)
                {
                    DataLayer.DataLayerFacade.WebRepository().updateProfileLike(RequestedProfileID);
                    return "Unsubscribed";
                }
                else
                {
                    return "Subscribe";
                }
            }
            else
            {
                result = DataLayer.DataLayerFacade.ConnectionRepository().Insert(ProfileID, RequestedProfileID, 2, false, DateTime.Now);
                return "Pending...";
            }
        }

        public IEnumerable<UserProfileDTO> getAdministrationAgencyProfile(string UserEmail)
        {
            return new UserProfileFacade().GetAdministrationUserProfile(UserEmail); 
        }

        public IEnumerable<UserProfileDTO> getAdministrationCountryProfile(int UserID)
        {
            UserDTO objUser = new UserFacade().GetUser(UserID);
            if(objUser != null)
            {
                if(objUser.CountryID != null)
                {
                    return new UserProfileFacade().GetAdministrationCountryUserProfile(objUser.CountryID.Value);
                }
            }
            return null;
        }

        public IEnumerable<ProfileDTO> getAdministrationCountryProfiles(int UserID)
        {
            UserDTO objUser = new UserFacade().GetUser(UserID);
            if (objUser != null)
            {
                if (objUser.CountryID != null)
                {
                    return Mapper.Map<List<ProfileDTO>>(DataLayer.DataLayerFacade.ProfileRepository().GetProfilesAllAsQueryable().Where(m => m.Addresses.Any(a => a.City.CountryID == objUser.CountryID.Value)));
                }
            }
            return null;
        }

        public IEnumerable<UserProfileDTO> getUserProfilesAgency(int UserID)
        {
            return new UserProfileFacade().GetUserProfileAgency(UserID);
        }

        public IEnumerable<UserProfileDTO> getUserProfilesAgencyForProfile(int ProfileID)
        {
            try
            {
                int userID = new UserProfileFacade().GetUserProfile(ProfileID, 1).UserID;
                return new UserProfileFacade().GetUserProfileAgency(userID);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        public UserProfileDTO getAdministrationAgencyProfileValidation(int UserID, int ProfileID)
        {
            return new UserProfileFacade().GetAdministrationUserProfileValidation(UserID,ProfileID);
        }

        public IEnumerable<AboutModel> getAbout(int ProfileID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int32;
            paramPageNumber.Value = ProfileID;
            paramPageNumber.ParameterName = "@ProfileID";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramPageNumber);

            var r = new WebRepository().getStoredProcedure<AboutModel>("[dbo].[getAbout]", param);
            return (IEnumerable<AboutModel>)r[0];
        }

        public AboutPageModel getAboutPage(string ProfileURL, int RequestedProfileID, int pagenumber, int pagesize)
        {
            AboutPageModel result = new AboutPageModel();

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileURL = new SqlParameter();
            paramProfileURL.DbType = System.Data.DbType.String;
            paramProfileURL.Value = ProfileURL;
            paramProfileURL.ParameterName = "@ProfileURL";
            paramProfileURL.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramRequestedProfileID = new SqlParameter();
            paramRequestedProfileID.DbType = System.Data.DbType.Int32;
            paramRequestedProfileID.Value = RequestedProfileID;
            paramRequestedProfileID.ParameterName = "@RequestedProfileID";
            paramRequestedProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileURL);
            param.Add(paramPageNumber);
            param.Add(paramPageSize);
            param.Add(paramRequestedProfileID);

            var r = new WebRepository().getStoredProcedure<ProfileSemiModel, AboutModel, AlbumModel, SocialNetworkModel>("[dbo].[getAboutByURL]", param);
            IEnumerable<ProfileSemiModel> profiles = (IEnumerable<ProfileSemiModel>)r[0];
            result.Profile = profiles.FirstOrDefault();
            result.Abouts = (IEnumerable<AboutModel>)r[1];
            result.Albums = (IEnumerable<AlbumModel>)r[2];
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[3];

            return result;
        }

        public AddressPageModel getAddressPage(string ProfileURL, int? AddressTypeID)
        {
            AddressPageModel result = new AddressPageModel();

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileURL = new SqlParameter();
            paramProfileURL.DbType = System.Data.DbType.String;
            paramProfileURL.Value = ProfileURL;
            paramProfileURL.ParameterName = "@ProfileURL";
            paramProfileURL.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramAddressTypeID = new SqlParameter();
            paramAddressTypeID.DbType = System.Data.DbType.Int16;
            paramAddressTypeID.Value = AddressTypeID;
            paramAddressTypeID.ParameterName = "@AddressTypeID";
            paramAddressTypeID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileURL);
            param.Add(paramAddressTypeID);

            var r = new WebRepository().getStoredProcedure<AddressModelV3, AddressTypeModel>("[dbo].[getAddressesByURL]", param);
            result.Addresses = (IEnumerable<AddressModelV3>)r[0];
            result.AddressTypes = (IEnumerable<AddressTypeModel>)r[1];

            return result;
        }

        public IEnumerable<DataTransferObjects.Mobile.AddressModel> getAddressMobile(int ProfileID)
        {
            IEnumerable<DataTransferObjects.Mobile.AddressModel> result;

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int32;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);            

            var r = new WebRepository().getStoredProcedure<DataTransferObjects.Mobile.AddressModel>("[dbo].[getAddressesAll]", param);
            result = (IEnumerable<DataTransferObjects.Mobile.AddressModel>)r[0];

            return result;
        }

        public IEnumerable<DataTransferObjects.Mobile.AddressModelV2> getAddressMobileV2(int ProfileID)
        {
            return DataLayer.DataLayerFacade.AddressRepository().GetAddressesByProfileID(ProfileID); 
        }

        public int createNewMobileUser()
        {            
            return DataLayer.DataLayerFacade.ProfileRepository().Insert().ProfileID;
        }

        public GalleryPageModel getGalleryPage(string ProfileURL, int pagenumber, int pagesize, int CategoryID, int OrderType)
        {
            GalleryPageModel result = new GalleryPageModel();

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileURL = new SqlParameter();
            paramProfileURL.DbType = System.Data.DbType.String;
            paramProfileURL.Value = ProfileURL;
            paramProfileURL.ParameterName = "@ProfileURL";
            paramProfileURL.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int16;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramCategoryID = new SqlParameter();
            paramCategoryID.DbType = System.Data.DbType.Int32;
            paramCategoryID.IsNullable = true;
            if (CategoryID != 0)
            {
                paramCategoryID.Value = CategoryID;
            }
            else
            {
                paramCategoryID.Value = DBNull.Value;
            }
            paramCategoryID.ParameterName = "@CategoryID";
            paramCategoryID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileURL);
            param.Add(paramCategoryID);
            param.Add(paramPageNumber);
            param.Add(paramPageSize);
            param.Add(paramOrderType);

            //var r = new WebRepository().getStoredProcedure<AlbumModel, CategoryModel, ProfileSemiModel, SocialNetworkModel>("[dbo].[getAlbumsByURL]", param);
            var r = new WebRepository().getStoredProcedure<AlbumModel, CategoryModel>("[dbo].[getAlbumsByURL]", param);

            //IEnumerable<ProfileSemiModel> profiles = (IEnumerable<ProfileSemiModel>)r[2];
            //result.Profile = profiles.FirstOrDefault();
            result.Categories = (IEnumerable<CategoryModel>)r[1];
            result.Albums = (IEnumerable<AlbumModel>)r[0];
            //result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[3];

            return result;
        }

        public LowProfileModel getLowProfile(string ProfileURL)
        {
            LowProfileModel result = new LowProfileModel();

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileURL = new SqlParameter();
            paramProfileURL.DbType = System.Data.DbType.String;
            paramProfileURL.Value = ProfileURL;
            paramProfileURL.ParameterName = "@ProfileURL";
            paramProfileURL.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileURL);

            var r = new WebRepository().getStoredProcedure<ProfileSemiModel, SocialNetworkModel>("[dbo].[getProfileLowByURL]", param);
            IEnumerable<ProfileSemiModel> profiles = (IEnumerable<ProfileSemiModel>)r[0];
            result.Profile = profiles.FirstOrDefault();
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[1];

            return result;
        }

        public LowAppProfileModel getLowAppProfile(string ProfileURL)
        {
            return DataLayer.DataLayerFacade.ProfileRepository().GetLowAppProfile(ProfileURL);
        }

        public LowProfileModel getLowProfile(string ProfileURL, int RequestedProfileID)
        {
            LowProfileModel result = new LowProfileModel();

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileURL = new SqlParameter();
            paramProfileURL.DbType = System.Data.DbType.String;
            paramProfileURL.Value = ProfileURL;
            paramProfileURL.ParameterName = "@ProfileURL";
            paramProfileURL.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int32;
            if (RequestedProfileID > 0)
            {
                paramProfileID.Value = RequestedProfileID;
            }
            else
            {
                paramProfileID.Value = 0;
            }
            paramProfileID.ParameterName = "@RequestedProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileURL);
            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<ProfileSemiModel, SocialNetworkModel>("[dbo].[getMyProfileLowByURL]", param);
            IEnumerable<ProfileSemiModel> profiles = (IEnumerable<ProfileSemiModel>)r[0];
            result.Profile = profiles.FirstOrDefault();
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[1];

            return result;
        }

        public MainEntities getMainEntities()
        {
            MainEntities objEntities = new MainEntities();
            var r = new WebRepository().getStoredProcedure<CountryModel, IndustryModel, ProfileTypeModel, TagModel>("[dbo].[getMainEntities]");
            objEntities.Country = (IEnumerable<CountryModel>)r[0];
            objEntities.Industry = (IEnumerable<IndustryModel>)r[1];
            objEntities.ProfileType = (IEnumerable<ProfileTypeModel>)r[2];
            objEntities.Tags = (IEnumerable<TagModel>)r[3];
            return objEntities;
        }

        public IEnumerable<CityModel> getCities()
        {
            var r = new WebRepository().getStoredProcedure<CityModel>("[dbo].[getCities]").FirstOrDefault();
            return (IEnumerable<CityModel>)r;
        }

        public IEnumerable<CategoryModel> getCategories()
        {
            var r = new WebRepository().getStoredProcedure<CategoryModel>("[dbo].[getCategories]").FirstOrDefault();
            return (IEnumerable<CategoryModel>)r;
        }

        public IEnumerable<DataTransferObjects.Mobile.CategoryMobileModel> getCategories(int ProfileID)
        {
            var r = DataLayer.DataLayerFacade.CategoryRepository().GetCategoriesAllAsQueryable(ProfileID);
            return r;
        }

        public IEnumerable<CountryModel> getCountries()
        {
            var r = new WebRepository().getStoredProcedure<CountryModel>("[dbo].[getCountries]").FirstOrDefault();
            return (IEnumerable<CountryModel>)r;
        }

        public IEnumerable<AddressTypeModel> getAddressTypes()
        {
            var r = new WebRepository().getStoredProcedure<AddressTypeModel>("[dbo].[getAddressTypes]").FirstOrDefault();
            return (IEnumerable<AddressTypeModel>)r;
        }

        public IEnumerable<IndustryModel> getIndustries()
        {
            var r = new WebRepository().getStoredProcedure<IndustryModel>("[dbo].[getIndustries]").FirstOrDefault();
            return (IEnumerable<IndustryModel>)r;
        }

        public IEnumerable<TagModel> getTags()
        {
            var r = new WebRepository().getStoredProcedure<TagModel>("[dbo].[getTags]").FirstOrDefault();
            return (IEnumerable<TagModel>)r;
        }

        public IEnumerable<TagModel> getTagsPrimary()
        {
            var r = new WebRepository().getStoredProcedure<TagModel>("[dbo].[getTagsPrimary]").FirstOrDefault();
            return (IEnumerable<TagModel>)r;
        }

        public IEnumerable<ProfileTypeModel> getProfileTypes()
        {
            var r = new WebRepository().getStoredProcedure<ProfileTypeModel>("[dbo].[getProfileTypes]").FirstOrDefault();
            return (IEnumerable<ProfileTypeModel>)r;
        }

        public ProfileListModel getProfileDetail(int ProfileID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<ProfileModel, AboutModel, ConnectionModel, ExternalLinkModel, SocialNetworkModel, TagModel>("[dbo].[getProfileDetail]", param);

            ProfileListModel result = new ProfileListModel();
            IEnumerable<ProfileModel> listProfile = (IEnumerable<ProfileModel>)r[0];
            result.Profile = (ProfileModel)listProfile.FirstOrDefault();
            result.About = (IEnumerable<AboutModel>)r[1];
            result.Connections = (IEnumerable<ConnectionModel>)r[2];
            result.ExternalLinks = (IEnumerable<ExternalLinkModel>)r[3];
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[4];
            result.Tags = (IEnumerable<TagModel>)r[5];
            //result.Addresses = (IEnumerable<AddressModel>)r[6];

            return result;
        }

        public DataTransferObjects.Mobile.ProfileSliderModel getMobileProfileDetailSlider (int ProfileID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int32;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<ProfileModel, TagModel>("[dbo].[getMobileSliderProfileByID]", param);

            DataTransferObjects.Mobile.ProfileSliderModel result = new DataTransferObjects.Mobile.ProfileSliderModel();
            IEnumerable<ProfileModel> listProfile = (IEnumerable<ProfileModel>)r[0];
            result.Profile = (ProfileModel)listProfile.FirstOrDefault();
            result.Tags = (IEnumerable<TagModel>)r[1];

            return result;
        }

        public ProfileSliderModel getProfileDetailSlider(int ProfileID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<ProfileModel, SocialNetworkModel, TagModel, ConnectionModel>("[dbo].[getSliderProfileByID]", param);

            ProfileSliderModel result = new ProfileSliderModel();
            IEnumerable<ProfileModel> listProfile = (IEnumerable<ProfileModel>)r[0];
            result.Profile = (ProfileModel)listProfile.FirstOrDefault();
            result.Connections = (IEnumerable<ConnectionModel>)r[3];
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[1];
            result.Tags = (IEnumerable<TagModel>)r[2];

            return result;
        }

        public ProfileListModel getProfileDetailByURL(string ProfileURL, int RequestedProfileID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.String;
            paramProfileID.Value = ProfileURL;
            paramProfileID.ParameterName = "@ProfileURL";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramRequestedProfileID = new SqlParameter();
            paramRequestedProfileID.DbType = System.Data.DbType.Int32;
            paramRequestedProfileID.Value = RequestedProfileID;
            paramRequestedProfileID.ParameterName = "@RequestedProfileID";
            paramRequestedProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);
            param.Add(paramRequestedProfileID);

            var r = new WebRepository().getStoredProcedure<ProfileModel, AboutModel, ConnectionModel, ExternalLinkModel, SocialNetworkModel, TagModel, TagModel, AlbumModel>("[dbo].[getProfileDetailByURL]", param);

            ProfileListModel result = new ProfileListModel();
            IEnumerable<ProfileModel> listProfile = (IEnumerable<ProfileModel>)r[0];
            result.Profile = (ProfileModel)listProfile.FirstOrDefault();
            result.About = (IEnumerable<AboutModel>)r[1];
            result.Connections = (IEnumerable<ConnectionModel>)r[2];
            result.ExternalLinks = (IEnumerable<ExternalLinkModel>)r[3];
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[4];
            result.PrimaryTags = (IEnumerable<TagModel>)r[5];
            result.Tags = (IEnumerable<TagModel>)r[6];
            result.Albums = (IEnumerable<AlbumModel>)r[7];

            return result;
        }

        public ProfileListModel getProfileDetailByURLWeb(string ProfileURL)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.String;
            paramProfileID.Value = ProfileURL;
            paramProfileID.ParameterName = "@ProfileURL";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<ProfileModel, ExternalLinkModel, SocialNetworkModel, TagModel, TagModel>("[dbo].[getProfileDetailByURLWeb]", param);

            ProfileListModel result = new ProfileListModel();
            IEnumerable<ProfileModel> listProfile = (IEnumerable<ProfileModel>)r[0];
            result.Profile = (ProfileModel)listProfile.FirstOrDefault();
            result.ExternalLinks = (IEnumerable<ExternalLinkModel>)r[1];
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[2];
            result.PrimaryTags = (IEnumerable<TagModel>)r[3];
            result.Tags = (IEnumerable<TagModel>)r[4];
            return result;
        }

        public IEnumerable<AboutModel> getProfileAboutUs(int ProfileID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<AboutModel>("[dbo].[getAbout]", param).FirstOrDefault();
            return (IEnumerable<AboutModel>)r;
        }

        //public IEnumerable<AddressModel> getProfileAddresses(int ProfileID, int? AddressTypeID)
        //{
        //    IList<SqlParameter> param = new List<SqlParameter>();

        //    SqlParameter paramProfileID = new SqlParameter();
        //    paramProfileID.DbType = System.Data.DbType.Int16;
        //    paramProfileID.Value = ProfileID;
        //    paramProfileID.ParameterName = "@ProfileID";
        //    paramProfileID.Direction = System.Data.ParameterDirection.Input;

        //    SqlParameter paramAddressTypeID = new SqlParameter();
        //    paramAddressTypeID.DbType = System.Data.DbType.Int16;
        //    paramAddressTypeID.Value = AddressTypeID;
        //    paramAddressTypeID.ParameterName = "@AddressTypeID";
        //    paramAddressTypeID.Direction = System.Data.ParameterDirection.Input;

        //    param.Add(paramAddressTypeID);
        //    param.Add(paramProfileID);

        //    var r = new WebRepository().getStoredProcedure<AddressModel>("[dbo].[getAddresses]", param).FirstOrDefault();
        //    return (IEnumerable<AddressModel>)r;
        //}

        public List<AddressList> getProfileAddresses(string ProfileURL, int AddressTypeID)
        {
            IQueryable<Address> x = null;

            if (AddressTypeID != 0)
            {
                x = DataLayer.DataLayerFacade.AddressRepository().GetAddressesAllAsQueryable(ProfileURL, AddressTypeID);
            }
            else
            {
                x = DataLayer.DataLayerFacade.AddressRepository().GetAddressesByProfileURLAsQueryable(ProfileURL);
            }

            HashSet<string> countries = new HashSet<string>();
            foreach (Address objAddress in x)
            {
                countries.Add(objAddress.City.Country.CountryName);
            }

            List<AddressList> result = new List<AddressList>();
            foreach (string country in countries)
            {
                AddressList objNewAddress = new AddressList();
                objNewAddress.CountryName = country;
                foreach (Address objAddress in x)
                {
                    if (objAddress.City.Country.CountryName == country)
                    {
                        objNewAddress.Addresses.Add(Mapper.Map<AddressModelV2>(objAddress));
                    }
                }
                result.Add(objNewAddress);
            }
            return result;
        }

        public IEnumerable<AlbumImageSetModel> getProfileAlbumImageSets(int ProfileID, int ImageRows, int pagenumber, int pagesize)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramImageRows = new SqlParameter();
            paramImageRows.DbType = System.Data.DbType.Int16;
            paramImageRows.Value = ImageRows;
            paramImageRows.ParameterName = "@ImageRows";
            paramImageRows.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramImageRows);
            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<AlbumImageSetModel>("[dbo].[getAlbumImageSets]", param).FirstOrDefault();
            return (IEnumerable<AlbumImageSetModel>)r;
        }

        public IEnumerable<AlbumModel> getProfileAlbums(int ProfileID, int OrderType, int CategoryID, int pagenumber, int pagesize)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileIsPeople = new SqlParameter();
            paramProfileIsPeople.DbType = System.Data.DbType.Int16;
            paramProfileIsPeople.Value = CategoryID;
            paramProfileIsPeople.ParameterName = "@CategoryID";
            paramProfileIsPeople.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int16;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramProfileIsPeople);
            param.Add(paramProfileID);
            param.Add(paramOrderType);

            var r = new WebRepository().getStoredProcedure<AlbumModel>("[dbo].[getAlbums]", param).FirstOrDefault();
            return (IEnumerable<AlbumModel>)r;
        }

        public IEnumerable<AlbumModel> getProfileAlbums(int ProfileID, int pagenumber, int pagesize)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<AlbumModel>("[dbo].[getLatestAlbums]", param).FirstOrDefault();
            return (IEnumerable<AlbumModel>)r;
        }

        public IEnumerable<ContentModel> getAlbum(int AlbumID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramAlbumID = new SqlParameter();
            paramAlbumID.DbType = System.Data.DbType.Int32;
            paramAlbumID.Value = AlbumID;
            paramAlbumID.ParameterName = "@AlbumID";
            paramAlbumID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramAlbumID);

            var r = new WebRepository().getStoredProcedure<ContentModel>("[dbo].[getContents]", param).FirstOrDefault();
            return (IEnumerable<ContentModel>)r;
        }

        public bool updateContentLike(int ContentID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramContentID = new SqlParameter();
            paramContentID.DbType = System.Data.DbType.Int32;
            paramContentID.Value = ContentID;
            paramContentID.ParameterName = "@ContentID";
            paramContentID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramContentID);            

            var r = new WebRepository().getStoredProcedure<ContentModel>("[dbo].[updateContentLike]", param);
            return true;
        }

        public bool updateContentLike(IList<DataTransferObjects.Mobile.ContentLikesModel> ContentLikes)
        {
            foreach(DataTransferObjects.Mobile.ContentLikesModel obj in ContentLikes)
            {
                DataLayer.DataLayerFacade.ContentRepository().Update(obj.ContentID, obj.ContentLikes);
            }            
            return true;
        }

        public IEnumerable<ProfileMobileModel> getAllProfiles()
        {            
            var r = new WebRepository().getStoredProcedure<ProfileMobileModel>("[dbo].[getAllProfiles]").FirstOrDefault();
            return (IEnumerable<ProfileMobileModel>)r;
        }

        public Infrastruture.Core.PaginationSet<ProfileMobileModel> getAllProfiles(int PageSize, int PageNumber)
        {
            int intTotalNumber = 0;
            if(PageNumber == 1)
            {
                intTotalNumber = getAllProfiles().Count();
            }

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = PageNumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = PageSize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramPageSize);
            param.Add(paramPageNumber);

            var r = new WebRepository().getStoredProcedure<ProfileMobileModel>("[dbo].[getAllProfilesByPaging]", param).FirstOrDefault();

            Infrastruture.Core.PaginationSet<ProfileMobileModel> result = new Infrastruture.Core.PaginationSet<ProfileMobileModel>()
            {
                Page = PageNumber,
                TotalCount = intTotalNumber,
                TotalPages = (int)Math.Ceiling((decimal)intTotalNumber / PageSize),
                Items = (List<ProfileMobileModel>)((IEnumerable<ProfileMobileModel>)r)
            };

            return result;
        }

        public IEnumerable<ProfileMobileModel> getAllMyConnectedProfiles(int ProfileID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int32;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<ProfileMobileModel>("[dbo].[getAllMyConnectedProfiles]", param).FirstOrDefault();
            return (IEnumerable<ProfileMobileModel>)r;
        }

        public bool updateAlbumView(int AlbumID)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().Update(AlbumID);
        }

        public DataTransferObjects.Mobile.AlbumMobileModel getHottestAlbum(int AlbumID)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().GetAlbumContent(AlbumID);
        }

        public IList<DataTransferObjects.Mobile.AlbumMobileModel> getLatestMomentAlbums(int ProfileID, int PageSize, int PageNumber, int ContentSize)
        {            
            return DataLayer.DataLayerFacade.AlbumRepository().GetAlbumContentsAsQueryable(ProfileID, PageSize, PageNumber, ContentSize);
        }

        public IList<DataTransferObjects.Mobile.AlbumMobileAdminModel> getLatestMomentAdminAlbums(int ProfileID, int PageSize, int PageNumber, int ContentSize)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().GetAdminAlbumContentsAsQueryable(ProfileID, PageSize, PageNumber, ContentSize);
        }

        public async System.Threading.Tasks.Task<IList<DataTransferObjects.Mobile.AlbumMobileAdminModel>> getLatestMomentAdminAlbumsAsync(int ProfileID, int PageSize, int PageNumber, int ContentSize)
        {
            return await DataLayer.DataLayerFacade.AlbumRepository().GetAdminAlbumContentsAsQueryableAsync(ProfileID, PageSize, PageNumber, ContentSize);
        }

        public IList<DataTransferObjects.Mobile.AlbumMobileModel> getLatestHottestAlbums(int ProfileID, int CategoryID, int PageSize, int PageNumber)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().GetAlbumHottestContentsAsQueryable(ProfileID, CategoryID, PageSize, PageNumber);
        }

        public IList<DataTransferObjects.Mobile.AlbumMobileModel> getLatestGalleryAlbums(int ProfileID, int CategoryID, int PageSize, int PageNumber)
        {
            if(CategoryID == 0)
            {
                return DataLayer.DataLayerFacade.AlbumRepository().GetAlbumGalleryContentsAsQueryable(ProfileID, PageSize, PageNumber);
            }
            else
            {
                return DataLayer.DataLayerFacade.AlbumRepository().GetAlbumGalleryContentsAsQueryable(ProfileID, CategoryID, PageSize, PageNumber);
            }
        }

        public IEnumerable<ContentItemModel> getLatestContents(int ProfileID, int PageSize, int PageNumber)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = PageNumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = PageSize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<ContentItemModel>("[dbo].[getLatestContents]", param).FirstOrDefault();
            return (IEnumerable<ContentItemModel>)r;
        }

        public FreshProfiles getMainLoadProfiles(int OrderType, int? CountryID, string[] Industries, int? ProfileTypeID, string[] Tags, string Location)
        {
            FreshProfiles result = new FreshProfiles();

            result.Webs = getProfiles(1, 1, 16, OrderType, CountryID, Industries, ProfileTypeID, Tags, Location);
            result.Apps = getProfiles(2, 1, 20, OrderType, CountryID, Industries, ProfileTypeID, Tags, Location);
            result.Sliders = getProfiles(3, 1, 24, OrderType, CountryID, Industries, ProfileTypeID, Tags, Location);

            return result;
        }

        public FreshProfiles getMainLoadProfiles(int OrderType, int? CountryID, string[] Industries, int? ProfileTypeID, string[] Tags, string Location, int? ProfileID)
        {
            FreshProfiles result = new FreshProfiles();

            result.Webs = getMyProfiles(ProfileID, true, null, 1, 16, OrderType, CountryID, Industries, ProfileTypeID, Tags, Location);
            result.Apps = getMyProfiles(ProfileID, false, null, 4, 20, OrderType, CountryID, Industries, ProfileTypeID, Tags, Location);
            if (Tags.Length > 0)
            {
                result.Sliders = getMyFriendsProfiles(1003, false, 4, 1, 20, OrderType, CountryID, Industries, ProfileTypeID, Tags, Location);
            }
            else {
                result.Sliders = getMyFriendsProfilesWithoutTags(1003, false, 4, 1, 20, OrderType, CountryID, Industries, ProfileTypeID, Location);
            }

            return result;
        }

        public AdminMiniProfile getMiniProfile(int ProfileID)
        {
            return DataLayer.DataLayerFacade.ProfileRepository().GetMiniProfile(ProfileID);
        }

        public IEnumerable<ProfileItemModel> getProfiles(int? SubscriptionTypeID, int pagenumber, int pagesize, int OrderType, int? CountryID, string[] Industries, int? ProfileTypeID, string[] Tags, string Location)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = SubscriptionTypeID;
            paramProfileID.ParameterName = "@SubscriptionTypeID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramIndustryID = new SqlParameter();
            paramIndustryID.DbType = System.Data.DbType.String;
            if (Industries != null && Industries.Length > 0)
            {
                string resultIndustries = "";
                foreach (string obj in Industries)
                {
                    resultIndustries = resultIndustries + obj + ",";
                }
                paramIndustryID.Value = resultIndustries;
            }
            else
            {
                paramIndustryID.Value = DBNull.Value;
            }
            paramIndustryID.ParameterName = "@IndustryID";
            paramIndustryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileTypeID = new SqlParameter();
            paramProfileTypeID.DbType = System.Data.DbType.Int16;
            paramProfileTypeID.Value = ProfileTypeID;
            paramProfileTypeID.ParameterName = "@ProfileTypeID";
            paramProfileTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramCountryID = new SqlParameter();
            paramCountryID.DbType = System.Data.DbType.Int16;
            paramCountryID.Value = CountryID;
            paramCountryID.ParameterName = "@CountryID";
            paramCountryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int16;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramLocation = new SqlParameter();
            paramLocation.DbType = System.Data.DbType.String;
            if (Location != null && !Location.Equals(""))
            {
                paramLocation.Value = Location;
            }
            else
            {
                paramLocation.Value = DBNull.Value;
            }
            paramLocation.ParameterName = "@Location";
            paramLocation.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramTags = new SqlParameter();
            paramTags.DbType = System.Data.DbType.String;
            if (Tags != null && Tags.Length > 0)
            {
                string resultTags = "";
                foreach (string obj in Tags)
                {
                    resultTags = resultTags + obj + ",";
                }
                paramTags.Value = resultTags;
            }
            else
            {
                paramTags.Value = DBNull.Value;
            }
            paramTags.ParameterName = "@Tags";
            paramTags.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramTags);
            param.Add(paramLocation);
            param.Add(paramOrderType);
            param.Add(paramCountryID);
            param.Add(paramProfileTypeID);
            param.Add(paramProfileID);
            param.Add(paramIndustryID);

            var r = new WebRepository().getStoredProcedure<ProfileItemModel>("[dbo].[getProfiles]", param).FirstOrDefault();
            return (IEnumerable<ProfileItemModel>)r;
        }

        public FreshProfiles getProfileEntities(int OrderType, int? CountryID, string[] Industries, int? ProfileTypeID, string[] Tags, string Location)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramIndustryID = new SqlParameter();
            paramIndustryID.DbType = System.Data.DbType.String;
            if (Industries != null && Industries.Length > 0)
            {
                string resultIndustries = "";
                foreach (string obj in Industries)
                {
                    resultIndustries = resultIndustries + obj + ",";
                }
                paramIndustryID.Value = resultIndustries;
            }
            else
            {
                paramIndustryID.Value = DBNull.Value;
            }
            paramIndustryID.ParameterName = "@IndustryID";
            paramIndustryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileTypeID = new SqlParameter();
            paramProfileTypeID.DbType = System.Data.DbType.Int16;
            paramProfileTypeID.Value = ProfileTypeID;
            paramProfileTypeID.ParameterName = "@ProfileTypeID";
            paramProfileTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramCountryID = new SqlParameter();
            paramCountryID.DbType = System.Data.DbType.Int16;
            paramCountryID.Value = CountryID;
            paramCountryID.ParameterName = "@CountryID";
            paramCountryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int16;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramLocation = new SqlParameter();
            paramLocation.DbType = System.Data.DbType.String;
            if (Location != null && !Location.Equals(""))
            {
                paramLocation.Value = Location;
            }
            else
            {
                paramLocation.Value = DBNull.Value;
            }
            paramLocation.ParameterName = "@Location";
            paramLocation.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramTags = new SqlParameter();
            paramTags.DbType = System.Data.DbType.String;
            if (Tags != null && Tags.Length > 0)
            {
                string resultTags = "";
                foreach (string obj in Tags)
                {
                    resultTags = resultTags + obj + ",";
                }
                paramTags.Value = resultTags;
            }
            else
            {
                paramTags.Value = DBNull.Value;
            }
            paramTags.ParameterName = "@Tags";
            paramTags.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramTags);
            param.Add(paramLocation);
            param.Add(paramOrderType);
            param.Add(paramCountryID);
            param.Add(paramProfileTypeID);
            param.Add(paramIndustryID);

            var r = new WebRepository().getStoredProcedure<ProfileItemModel, ProfileItemModel, ProfileItemModel>("[dbo].[getProfileEntities]", param);
            FreshProfiles resultProfile = new FreshProfiles();
            resultProfile.Webs = (IEnumerable<ProfileItemModel>)r[0];
            resultProfile.Apps = (IEnumerable<ProfileItemModel>)r[1];
            resultProfile.Sliders = (IEnumerable<ProfileItemModel>)r[2];

            return resultProfile;
        }

        public IEnumerable<ProfileItemModel> getMyFriendsProfiles(int? ProfileID, bool ProfileIsConfirmed, int? SubscriptionTypeID, int pagenumber, int pagesize, int OrderType, int? CountryID, string[] Industries, int? ProfileTypeID, string[] Tags, string Location)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramSubscriptionTypeID = new SqlParameter();
            paramSubscriptionTypeID.DbType = System.Data.DbType.Int16;
            if (SubscriptionTypeID != null)
            {
                paramSubscriptionTypeID.Value = SubscriptionTypeID;
            }
            else
            {
                paramSubscriptionTypeID.Value = DBNull.Value;
            }
            paramSubscriptionTypeID.ParameterName = "@SubscriptionTypeID";
            paramSubscriptionTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileIsConfirmed = new SqlParameter();
            paramProfileIsConfirmed.DbType = System.Data.DbType.Boolean;
            paramProfileIsConfirmed.Value = ProfileIsConfirmed;
            paramProfileIsConfirmed.ParameterName = "@ProfileIsConfirmed";
            paramProfileIsConfirmed.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramIndustryID = new SqlParameter();
            paramIndustryID.DbType = System.Data.DbType.Int16;
            if (Industries != null && Industries.Length > 0)
            {
                string resultIndustries = "";
                foreach (string obj in Industries)
                {
                    resultIndustries = resultIndustries + obj + ",";
                }
                paramIndustryID.Value = resultIndustries;
            }
            else
            {
                paramIndustryID.Value = DBNull.Value;
            }
            paramIndustryID.ParameterName = "@IndustryID";
            paramIndustryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileTypeID = new SqlParameter();
            paramProfileTypeID.DbType = System.Data.DbType.Int16;
            paramProfileTypeID.Value = ProfileTypeID;
            paramProfileTypeID.ParameterName = "@ProfileTypeID";
            paramProfileTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramCountryID = new SqlParameter();
            paramCountryID.DbType = System.Data.DbType.Int16;
            paramCountryID.Value = CountryID;
            paramCountryID.ParameterName = "@CountryID";
            paramCountryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int16;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramLocation = new SqlParameter();
            paramLocation.DbType = System.Data.DbType.String;
            if (Location != null && !Location.Equals(""))
            {
                paramLocation.Value = Location;
            }
            else
            {
                paramLocation.Value = DBNull.Value;
            }
            paramLocation.ParameterName = "@Location";
            paramLocation.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramTags = new SqlParameter();
            paramTags.DbType = System.Data.DbType.String;
            if (Tags != null && Tags.Length > 0)
            {
                string resultTags = "";
                foreach (string obj in Tags)
                {
                    resultTags = resultTags + obj + ",";
                }
                paramTags.Value = resultTags;
            }
            else
            {
                paramTags.Value = DBNull.Value;
            }
            paramTags.ParameterName = "@Tags";
            paramTags.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramSubscriptionTypeID);
            param.Add(paramProfileIsConfirmed);
            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramTags);
            param.Add(paramLocation);
            param.Add(paramOrderType);
            param.Add(paramCountryID);
            param.Add(paramProfileTypeID);
            param.Add(paramProfileID);
            param.Add(paramIndustryID);

            var r = new WebRepository().getStoredProcedure<ProfileItemModel>("[dbo].[getMyFriendsProfiles]", param).FirstOrDefault();
            return (IEnumerable<ProfileItemModel>)r;
        }

        public IEnumerable<ProfileItemModel> getMyFriendsProfilesWithoutTags(int? ProfileID, bool ProfileIsConfirmed, int? SubscriptionTypeID, int pagenumber, int pagesize, int OrderType, int? CountryID, string[] Industries, int? ProfileTypeID, string Location)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramSubscriptionTypeID = new SqlParameter();
            paramSubscriptionTypeID.DbType = System.Data.DbType.Int16;
            if (SubscriptionTypeID != null)
            {
                paramSubscriptionTypeID.Value = SubscriptionTypeID;
            }
            else
            {
                paramSubscriptionTypeID.Value = DBNull.Value;
            }
            paramSubscriptionTypeID.ParameterName = "@SubscriptionTypeID";
            paramSubscriptionTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileIsConfirmed = new SqlParameter();
            paramProfileIsConfirmed.DbType = System.Data.DbType.Boolean;
            paramProfileIsConfirmed.Value = ProfileIsConfirmed;
            paramProfileIsConfirmed.ParameterName = "@ProfileIsConfirmed";
            paramProfileIsConfirmed.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramIndustryID = new SqlParameter();
            paramIndustryID.DbType = System.Data.DbType.Int16;
            if (Industries != null && Industries.Length > 0)
            {
                string resultIndustries = "";
                foreach (string obj in Industries)
                {
                    resultIndustries = resultIndustries + obj + ",";
                }
                paramIndustryID.Value = resultIndustries;
            }
            else
            {
                paramIndustryID.Value = DBNull.Value;
            }
            paramIndustryID.ParameterName = "@IndustryID";
            paramIndustryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileTypeID = new SqlParameter();
            paramProfileTypeID.DbType = System.Data.DbType.Int16;
            paramProfileTypeID.Value = ProfileTypeID;
            paramProfileTypeID.ParameterName = "@ProfileTypeID";
            paramProfileTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramCountryID = new SqlParameter();
            paramCountryID.DbType = System.Data.DbType.Int16;
            paramCountryID.Value = CountryID;
            paramCountryID.ParameterName = "@CountryID";
            paramCountryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int16;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramLocation = new SqlParameter();
            paramLocation.DbType = System.Data.DbType.String;
            if (Location != null && !Location.Equals(""))
            {
                paramLocation.Value = Location;
            }
            else
            {
                paramLocation.Value = DBNull.Value;
            }
            paramLocation.ParameterName = "@Location";
            paramLocation.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramSubscriptionTypeID);
            param.Add(paramProfileIsConfirmed);
            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramLocation);
            param.Add(paramOrderType);
            param.Add(paramCountryID);
            param.Add(paramProfileTypeID);
            param.Add(paramProfileID);
            param.Add(paramIndustryID);

            var r = new WebRepository().getStoredProcedure<ProfileItemModel>("[dbo].[getMyFriendsProfilesWithoutTag]", param).FirstOrDefault();
            return (IEnumerable<ProfileItemModel>)r;
        }

        public IEnumerable<ProfileItemModel> getMyProfiles(int? ProfileID, bool ProfileIsConfirmed, int? SubscriptionTypeID, int pagenumber, int pagesize, int OrderType, int? CountryID, string[] Industries, int? ProfileTypeID, string[] Tags, string Location)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramSubscriptionTypeID = new SqlParameter();
            paramSubscriptionTypeID.DbType = System.Data.DbType.Int16;
            if (SubscriptionTypeID != null)
            {
                paramSubscriptionTypeID.Value = SubscriptionTypeID;
            }
            else
            {
                paramSubscriptionTypeID.Value = DBNull.Value;
            }
            paramSubscriptionTypeID.ParameterName = "@SubscriptionTypeID";
            paramSubscriptionTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileIsConfirmed = new SqlParameter();
            paramProfileIsConfirmed.DbType = System.Data.DbType.Boolean;
            paramProfileIsConfirmed.Value = ProfileIsConfirmed;
            paramProfileIsConfirmed.ParameterName = "@ProfileIsConfirmed";
            paramProfileIsConfirmed.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramIndustryID = new SqlParameter();
            paramIndustryID.DbType = System.Data.DbType.String;
            if (Industries != null && Industries.Length > 0)
            {
                string resultIndustries = "";
                foreach (string obj in Industries)
                {
                    resultIndustries = resultIndustries + obj + ",";
                }
                paramIndustryID.Value = resultIndustries;
            }
            else
            {
                paramIndustryID.Value = DBNull.Value;
            }
            paramIndustryID.ParameterName = "@IndustryID";
            paramIndustryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileTypeID = new SqlParameter();
            paramProfileTypeID.DbType = System.Data.DbType.Int16;
            paramProfileTypeID.Value = ProfileTypeID;
            paramProfileTypeID.ParameterName = "@ProfileTypeID";
            paramProfileTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramCountryID = new SqlParameter();
            paramCountryID.DbType = System.Data.DbType.Int16;
            paramCountryID.Value = CountryID;
            paramCountryID.ParameterName = "@CountryID";
            paramCountryID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int16;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramLocation = new SqlParameter();
            paramLocation.DbType = System.Data.DbType.String;
            if (Location != null && !Location.Equals(""))
            {
                paramLocation.Value = Location;
            }
            else
            {
                paramLocation.Value = DBNull.Value;
            }
            paramLocation.ParameterName = "@Location";
            paramLocation.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramTags = new SqlParameter();
            paramTags.DbType = System.Data.DbType.String;
            if (Tags != null && Tags.Length > 0)
            {
                string resultTags = "";
                foreach (string obj in Tags)
                {
                    resultTags = resultTags + obj + ",";
                }
                paramTags.Value = resultTags;
            }
            else
            {
                paramTags.Value = DBNull.Value;
            }
            paramTags.ParameterName = "@Tags";
            paramTags.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramSubscriptionTypeID);
            param.Add(paramProfileIsConfirmed);
            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramTags);
            param.Add(paramLocation);
            param.Add(paramOrderType);
            param.Add(paramCountryID);
            param.Add(paramProfileTypeID);
            param.Add(paramProfileID);
            param.Add(paramIndustryID);

            var r = new WebRepository().getStoredProcedure<ProfileItemModel>("[dbo].[getMyProfiles]", param).FirstOrDefault();
            return (IEnumerable<ProfileItemModel>)r;
        }
        public IEnumerable<AlbumModel> getProfileLatestAlbums(int ProfileID, int pagenumber, int pagesize)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramProfileID);

            var r = new WebRepository().getStoredProcedure<AlbumModel>("[dbo].[getLatestAlbums]", param).FirstOrDefault();
            return (IEnumerable<AlbumModel>)r;
        }

        public IEnumerable<ConnectionModel> getProfileConnections(int ProfileID, int ProfileIsPeople, int ProfileStatus, int RequestTypeID, int pagenumber, int pagesize)
        {
            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.Int16;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileIsPeople = new SqlParameter();
            paramProfileIsPeople.DbType = System.Data.DbType.Int16;
            paramProfileIsPeople.Value = ProfileIsPeople;
            paramProfileIsPeople.ParameterName = "@ProfileIsPeople";
            paramProfileIsPeople.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramRequestTypeID = new SqlParameter();
            paramRequestTypeID.DbType = System.Data.DbType.Int16;
            paramRequestTypeID.Value = RequestTypeID;
            paramRequestTypeID.ParameterName = "@RequestTypeID";
            paramRequestTypeID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramProfileStatus = new SqlParameter();
            paramProfileStatus.DbType = System.Data.DbType.Int16;
            paramProfileStatus.Value = ProfileStatus;
            paramProfileStatus.ParameterName = "@ProfileStatus";
            paramProfileStatus.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageNumber = new SqlParameter();
            paramPageNumber.DbType = System.Data.DbType.Int16;
            paramPageNumber.Value = pagenumber;
            paramPageNumber.ParameterName = "@PageNumber";
            paramPageNumber.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramPageSize = new SqlParameter();
            paramPageSize.DbType = System.Data.DbType.Int16;
            paramPageSize.Value = pagesize;
            paramPageSize.ParameterName = "@PageSize";
            paramPageSize.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramPageSize);
            param.Add(paramPageNumber);
            param.Add(paramProfileStatus);
            param.Add(paramProfileIsPeople);
            param.Add(paramProfileID);
            param.Add(paramRequestTypeID);

            var r = new WebRepository().getStoredProcedure<ConnectionModel>("[dbo].[getProfileConnections]", param).FirstOrDefault();
            return (IEnumerable<ConnectionModel>)r;
        }

        public FeatureListModel getFeatures()
        {
            IList<SqlParameter> param = new List<SqlParameter>();
            SqlParameter param1 = new SqlParameter();
            param1.DbType = System.Data.DbType.Int16;
            param1.Value = 1;
            param1.ParameterName = "@FeaturedRole";
            param1.Direction = System.Data.ParameterDirection.Input;
            param.Add(param1);

            var r = new WebRepository().getStoredProcedure<ProfileModel, ExternalLinkModel, SocialNetworkModel, TagModel, FeatureModel, SponsoredFeatureModel, FeatureModel>("[dbo].[getFeatures]", param);
            FeatureListModel result = new FeatureListModel();
            IEnumerable<ProfileModel> listProfile = (IEnumerable<ProfileModel>)r[0];
            result.Profile = listProfile.FirstOrDefault();
            result.ExternalLinks = (IEnumerable<ExternalLinkModel>)r[1];
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[2];
            result.Tags = (IEnumerable<TagModel>)r[3];
            result.CurrentFeature = (IEnumerable<FeatureModel>)r[4];
            result.SponsoredFeature = (IEnumerable<SponsoredFeatureModel>)r[5];
            result.ArchiveFeatures = (IEnumerable<FeatureModel>)r[6];

            return result;
        }

        public FeatureDetailModel getFeatureByID(int FeatureID)
        {
            IList<SqlParameter> param = new List<SqlParameter>();
            SqlParameter paramFeatureID = new SqlParameter();
            paramFeatureID.DbType = System.Data.DbType.Int32;
            paramFeatureID.Value = FeatureID;
            paramFeatureID.ParameterName = "@FeatureID";
            paramFeatureID.Direction = System.Data.ParameterDirection.Input;
            param.Add(paramFeatureID);

            var r = new WebRepository().getStoredProcedure<ProfileModel, ExternalLinkModel, SocialNetworkModel, TagModel, FeatureModel>("[dbo].[getFeatureDetailByID]", param);
            FeatureDetailModel result = new FeatureDetailModel();
            IEnumerable<ProfileModel> listProfile = (IEnumerable<ProfileModel>)r[0];
            result.Profile = listProfile.FirstOrDefault();
            result.ExternalLinks = (IEnumerable<ExternalLinkModel>)r[1];
            result.SocialNetworks = (IEnumerable<SocialNetworkModel>)r[2];
            result.Tags = (IEnumerable<TagModel>)r[3];
            result.CurrentFeature = (IEnumerable<FeatureModel>)r[4];

            return result;
        }

        public bool setupNewUserProfile(string UserName, string UserFullname, int ProfileTypeID)
        {
            UserDTO result = Facade.UserFacade().Insert(UserFullname, UserName);
            ProfileDTO newProfile = new ProfileDTO();
            newProfile.ProfileCredit = 0;
            newProfile.ProfileDateCreated = DateTime.Now;
            newProfile.ProfileDescription = "";
            newProfile.ProfileEmail = UserName;
            newProfile.ProfileFullname = UserFullname;
            newProfile.ProfileImage = "NA";
            newProfile.ProfileIsConfirmed = false;
            newProfile.ProfileIsDeleted = false;
            newProfile.ProfileIsPeople = false;
            newProfile.ProfileLike = 0;
            newProfile.ProfileShortDescription = "";
            newProfile.ProfileStatus = true;
            newProfile.ProfileTypeID = ProfileTypeID;
            newProfile.ProfileURL = UserFullname.Replace(" ", ".").ToLower();
            newProfile.ProfileView = 0;
            newProfile.SubscriptionTypeID = 1;
            newProfile.ThemeID = 1;
            ProfileDTO resultProfile = Facade.ProfileFacade().Insert(newProfile,result.UserID);
            //Facade.WebFacade().GenerateQRCode(resultProfile.ProfileID);

            return true;
        }


    }
}