using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
namespace Gorgias.DataLayer.Repository.SQL
{
    public class ProfileRepository : IProfileRepository, IDisposable
    {
        // To detect redundant calls
        private bool disposedValue = false;

        private GorgiasEntities context = new GorgiasEntities();

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }
                }
                this.disposedValue = true;
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        //CRUD Functions
        public Profile Insert(String ProfileFullname, string ProfileFullnameEnglish, Boolean ProfileIsPeople, Boolean ProfileIsDeleted, DateTime ProfileDateCreated, String ProfileDescription, int ProfileView, int ProfileLike, String ProfileURL, String ProfileShortDescription, String ProfileImage, String ProfileEmail, Boolean ProfileStatus, Boolean ProfileIsConfirmed, int ProfileTypeID, int ThemeID, int SubscriptionTypeID)
        {
            try
            {
                Profile obj = new Profile();
                obj.ProfileFullname = ProfileFullname;
                obj.ProfileFullnameEnglish = ProfileFullnameEnglish;
                obj.ProfileIsPeople = ProfileIsPeople;
                obj.ProfileIsDeleted = ProfileIsDeleted;
                obj.ProfileDateCreated = DateTime.Now;
                obj.ProfileDescription = ProfileDescription;
                obj.ProfileView = ProfileView;
                obj.ProfileLike = ProfileLike;
                obj.ProfileURL = ProfileURL;
                obj.ProfileShortDescription = ProfileShortDescription;
                //If want to use FB profile image possible to key in ;)
                obj.ProfileImage = "na";
                obj.ProfileEmail = ProfileEmail;
                obj.ProfileStatus = ProfileStatus;
                obj.ProfileIsConfirmed = ProfileIsConfirmed;
                //obj.IndustryID = IndustryID;
                obj.ProfileTypeID = ProfileTypeID;
                obj.ThemeID = ThemeID;
                obj.SubscriptionTypeID = SubscriptionTypeID;
                context.Profiles.Add(obj);
                context.SaveChanges();
                //Update after Insert for ID ;)
                //obj.ProfileImage = "profile-" + obj.ProfileID + ".jpg";
                obj.ProfileImage = "https://gorgiasasia.blob.core.windows.net/images/" + "profile-" + obj.ProfileID + ".jpg";
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Profile();
            }
        }

        public Profile Insert()
        {
            try
            {
                string username = DateTime.UtcNow.ToString("yyMMdd");
                Profile obj = new Profile();
                obj.ProfileFullname = username;
                obj.ProfileFullnameEnglish = username;
                obj.ProfileIsPeople = false;
                obj.ProfileIsDeleted = false;
                obj.ProfileDateCreated = DateTime.UtcNow;
                obj.ProfileDescription = username;
                obj.ProfileView = 0;
                obj.ProfileLike = 0;
                obj.ProfileURL = username;
                obj.ProfileShortDescription = username;
                //If want to use FB profile image possible to key in ;)
                obj.ProfileImage = "na";
                obj.ProfileEmail = username;
                obj.ProfileStatus = true;
                obj.ProfileIsConfirmed = false;
                //obj.IndustryID = IndustryID;
                obj.ProfileTypeID = 1;
                obj.ThemeID = 3;
                obj.SubscriptionTypeID = 4;
                context.Profiles.Add(obj);
                context.SaveChanges();
                //Update after Insert for ID ;)
                //obj.ProfileImage = "profile-" + obj.ProfileID + ".jpg";
                obj.ProfileImage = "https://gorgiasasia.blob.core.windows.net/images/" + "profile-" + obj.ProfileID + ".jpg";
                obj.ProfileURL = username + obj.ProfileID;
                obj.ProfileFullname = username + obj.ProfileID;
                obj.ProfileFullnameEnglish = username + obj.ProfileID;
                obj.ProfileEmail = username + obj.ProfileID + "@gorgias.com";

                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Profile();
            }
        }

        public Profile Insert(String ProfileFullname, string ProfileFullnameEnglish, Boolean ProfileIsPeople, Boolean ProfileIsDeleted, DateTime ProfileDateCreated, String ProfileDescription, int ProfileView, int ProfileLike, String ProfileURL, String ProfileShortDescription, String ProfileImage, String ProfileEmail, Boolean ProfileStatus, Boolean ProfileIsConfirmed, int ProfileTypeID, int ThemeID, int SubscriptionTypeID, int UserID)
        {
            try
            {
                Profile obj = new Profile();
                obj.ProfileFullname = ProfileFullname;
                obj.ProfileFullnameEnglish = ProfileFullnameEnglish;
                obj.ProfileIsPeople = ProfileIsPeople;
                obj.ProfileIsDeleted = ProfileIsDeleted;
                obj.ProfileDateCreated = DateTime.Now;
                obj.ProfileDescription = ProfileDescription;
                obj.ProfileView = ProfileView;
                obj.ProfileLike = ProfileLike;
                obj.ProfileURL = ProfileURL;
                obj.ProfileShortDescription = ProfileShortDescription;
                //If want to use FB profile image possible to key in ;)
                obj.ProfileImage = "na";
                obj.ProfileEmail = ProfileEmail;
                obj.ProfileStatus = ProfileStatus;
                obj.ProfileIsConfirmed = ProfileIsConfirmed;
                //obj.IndustryID = IndustryID;
                obj.ProfileTypeID = ProfileTypeID;
                obj.ThemeID = ThemeID;
                obj.SubscriptionTypeID = SubscriptionTypeID;

                context.Profiles.Add(obj);
                context.SaveChanges();
                //Update after Insert for ID ;)
                //Create New Profile and UserProfile ;)
                obj.UserProfiles.Add(new UserProfile { UserID = UserID, ProfileID = obj.ProfileID, UserRoleID = 1 });
                obj.ProfileImage = "https://gorgiasasia.blob.core.windows.net/images/" + "profile-" + obj.ProfileID + ".jpg";
                //obj.ProfileImage = "profile-" + obj.ProfileID + ".jpg";
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Profile();
            }
        }

        public Profile Insert(Profile profile)
        {
            try
            {
                Profile obj = new Profile();
                obj.ProfileFullname = profile.ProfileFullname;
                obj.ProfileFullnameEnglish = profile.ProfileFullnameEnglish;
                obj.ProfileIsPeople = profile.ProfileIsPeople;
                obj.ProfileIsDeleted = profile.ProfileIsDeleted;
                obj.ProfileDateCreated = DateTime.Now;
                obj.ProfileDescription = profile.ProfileDescription;
                obj.ProfileView = profile.ProfileView;
                obj.ProfileLike = profile.ProfileLike;
                obj.ProfileURL = profile.ProfileURL;
                obj.ProfileShortDescription = profile.ProfileShortDescription;
                obj.ProfileImage = "na";
                obj.ProfileEmail = profile.ProfileEmail;
                obj.ProfileStatus = profile.ProfileStatus;
                obj.ProfileIsConfirmed = profile.ProfileIsConfirmed;
                obj.ProfileTypeID = profile.ProfileTypeID;
                obj.ThemeID = profile.ThemeID;
                obj.SubscriptionTypeID = profile.SubscriptionTypeID;

                obj.Addresses = profile.Addresses;
                //obj.Industries = profile.Industries;
                //obj.ProfileTags = profile.ProfileTags;
                //obj.ProfileSocialNetworks = profile.ProfileSocialNetworks;

                context.Profiles.Add(obj);
                context.SaveChanges();
                //Update after Insert for ID ;)
                obj.ProfileImage = "https://gorgiasasia.blob.core.windows.net/images/" + "profile-" + obj.ProfileID + ".jpg";
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Profile();
            }
        }

        public bool Update(int ProfileID, String ProfileFullname, string ProfileFullnameEnglish, Boolean ProfileIsPeople, Boolean ProfileIsDeleted, DateTime ProfileDateCreated, String ProfileDescription, int ProfileView, int ProfileLike, String ProfileURL, String ProfileShortDescription, String ProfileImage, String ProfileEmail, Boolean ProfileStatus, Boolean ProfileIsConfirmed, int ProfileTypeID, int ThemeID, int SubscriptionTypeID)
        {
            Profile obj = new Profile();
            obj = (from w in context.Profiles where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Profiles.Attach(obj);

                obj.ProfileFullname = ProfileFullname;
                obj.ProfileFullnameEnglish = ProfileFullnameEnglish;
                obj.ProfileIsPeople = ProfileIsPeople;
                obj.ProfileIsDeleted = ProfileIsDeleted;
                obj.ProfileDateCreated = ProfileDateCreated;
                obj.ProfileDescription = ProfileDescription;
                obj.ProfileView = ProfileView;
                obj.ProfileLike = ProfileLike;
                obj.ProfileURL = ProfileURL;
                obj.ProfileShortDescription = ProfileShortDescription;
                obj.ProfileImage = ProfileImage;
                obj.ProfileEmail = ProfileEmail;
                obj.ProfileStatus = ProfileStatus;
                obj.ProfileIsConfirmed = ProfileIsConfirmed;
                //obj.IndustryID = IndustryID;
                obj.ProfileTypeID = ProfileTypeID;
                obj.ThemeID = ThemeID;
                obj.SubscriptionTypeID = SubscriptionTypeID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, string ProfileFullname, string ProfileFullnameEnglish, string ProfileShortDescription, string ProfileEmail, int ProfileTypeID, int IndustryID, int CityID)
        {
            Profile obj = new Profile();
            obj = (from w in context.Profiles where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                //Check Email is in system for registration ;)
                string emailProfile = ProfileEmail.ToLower();
                Profile resultEmailChecking = (from em in context.Profiles where em.ProfileEmail.ToLower().Equals(emailProfile) select em).FirstOrDefault();
                if(resultEmailChecking != null)
                {
                    return false;
                }

                context.Profiles.Attach(obj);

                obj.ProfileFullname = ProfileFullname;
                obj.ProfileFullnameEnglish = ProfileFullnameEnglish;
                obj.ProfileShortDescription = ProfileShortDescription;
                obj.ProfileEmail = ProfileEmail;
                //obj.IndustryID = IndustryID;
                obj.ProfileTypeID = ProfileTypeID;

                obj.Industries.Add((from x in context.Industries where x.IndustryID == IndustryID select x).First());                
                obj.Addresses.Add(new Address { AddressAddress = "NA", AddressEmail = ProfileEmail, AddressName = ProfileFullname, AddressStatus = true, AddressTypeID = 1, CityID = CityID, AddressTel = "NA" });                
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, string ProfileFullname)
        {
            Profile obj = new Profile();
            obj = (from w in context.Profiles where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Profiles.Attach(obj);

                obj.ProfileFullname = ProfileFullname;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, bool Status, int UpdateMode)
        {
            Profile obj = new Profile();
            obj = (from w in context.Profiles where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Profiles.Attach(obj);

                if (UpdateMode == 1)
                {
                    obj.ProfileIsConfirmed = Status;
                }
                else if (UpdateMode == 2)
                {
                    obj.ProfileIsPeople = Status;
                }
                else if (UpdateMode == 3)
                {
                    obj.ProfileStatus = Status;
                }
                else
                {
                    obj.ProfileIsDeleted = Status;
                }

                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ProfileID)
        {
            Profile obj = new Profile();
            obj = (from w in context.Profiles.Include("Connections").Include("ProfileAttributes").Include("ProfileTags").Include("UserProfiles").Include("Industries") where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                obj.Connections.Clear();
                obj.ProfileTags.Clear();
                obj.Industries.Clear();
                obj.ProfileAttributes.Clear();
                obj.UserProfiles.Clear();
                context.Profiles.Attach(obj);
                context.Profiles.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Profile GetProfile(int ProfileID)
        {
            return (from w in context.Profiles.Include("Theme").Include("SubscriptionType").Include("ProfileType") where w.ProfileID == ProfileID select w).FirstOrDefault();
        }

        //V2 Begin
        public Business.DataTransferObjects.Mobile.V2.MiniProfileMobileModel GetV2MiniMobileProfile(int ProfileID, int RequestedProfileID, string languageCode)
        {
            return (from w in context.Profiles where w.ProfileID == ProfileID select new Business.DataTransferObjects.Mobile.V2.MiniProfileMobileModel { CityName = w.Addresses.FirstOrDefault().City.CityName, IndustryName = w.Industries.FirstOrDefault().IndustryName, ProfileID = w.ProfileID, ProfileFullname = w.ProfileFullname, ProfileFullnameEnglish = w.ProfileFullnameEnglish, ProfileShortDescription = w.ProfileShortDescription, ProfileTypeName = w.ProfileType.ProfileTypeName, TotalConnections = w.Connections.Count, TotalEngagements = w.Albums.Sum(a=> a.ProfileActivities.Where(ac=> ac.ActivityTypeID != 1).Count()), TotalViews = w.Albums.Sum(av=> av.AlbumView), isSubscribed = w.Connections.Any(cc=> cc.Profile1.ProfileID == RequestedProfileID) }).FirstOrDefault();
        }
        //V2 Ends

        public IEnumerable<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrent()
        {
            //int currentDay = DateTime.UtcNow.Day;
            var result = (from w in context.Profiles
                          where w.SubscriptionTypeID != 4
                          select
                            new Business.DataTransferObjects.Report.ProfileReport
                            {
                            ProfileID = w.ProfileID,
                            ProfileView = w.ProfileView,
                            AlbumView = w.Albums.Sum(av => av.AlbumView),
                            AlbumComments = w.Albums.Sum(av => av.Contents.Sum(cv => cv.Comments.Count)),
                            AlbumLikes = w.Albums.Sum(av => av.Contents.Sum(cv => cv.ContentLike)),
                            Subscription = w.Connections.Where(sv => sv.RequestTypeID == 1).Count(),
                            StayOnConnection = w.Connections.Where(sv => sv.RequestTypeID == 3).Count()
                            }).ToList();
            return result;
        }

        public Int64 GetProfileReportCurrentProfileViews()
        {
            //int currentDay = DateTime.UtcNow.Day;
            var result = (from w in context.Profiles
                          where w.SubscriptionTypeID != 4
                          select
                            new Business.DataTransferObjects.Report.ProfileVisitReport
                            {
                                ProfileView = w.ProfileView, AlbumView = w.Albums.Sum(av => av.AlbumView)
                            }).ToList();
            return result.Sum(m=>m.ProfileVisit);
        }

        public IList<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrent(int UserID)
        {
            //int currentDay = DateTime.UtcNow.Day;
            var result = (from w in context.Profiles
                          where w.SubscriptionTypeID != 4 && w.UserProfiles.Any(up=> up.UserID == UserID)
                          select
                            new Business.DataTransferObjects.Report.ProfileReport
                            {
                                ProfileFullname = w.ProfileFullname,
                                ProfileID = w.ProfileID,
                                ProfileView = w.ProfileView,
                                AlbumView = w.Albums.Sum(av => av.AlbumView),
                                AlbumComments = w.Albums.Sum(av => av.Contents.Sum(cv => cv.Comments.Count)),
                                AlbumLikes = w.Albums.Sum(av => av.Contents.Sum(cv => cv.ContentLike)),
                                Subscription = w.Connections.Where(sv => sv.RequestTypeID == 1).Count(),
                                StayOnConnection = w.Connections.Where(sv => sv.RequestTypeID == 3).Count(),
                                OverAllRevenue = w.ProfileReports.Where(pp => pp.ReportTypeID == 1 || pp.ReportTypeID == 2).Sum(pps => pps.ProfileReportRevenue),
                                OverAllView = w.ProfileReports.Where(pp => pp.ReportTypeID == 1 || pp.ReportTypeID == 2).Sum(pps => pps.ProfileReportActivityCount),
                                OverAllEngagement = w.ProfileReports.Where(pp => pp.ReportTypeID == 3 || pp.ReportTypeID == 4).Sum(pps => pps.ProfileReportActivityCount),
                                OverAllSubscription = w.ProfileReports.Where(pp => pp.ReportTypeID == 5 || pp.ReportTypeID == 6).Sum(pps => pps.ProfileReportActivityCount),
                                ConnectedUserShare = w.ProfileCommissions.Where(pc => pc.UserRoleID != 1).Sum(spc => spc.ProfileCommissionRate),
                                UserCommission = w.ProfileCommissions.Where(pc => pc.UserID == UserID).FirstOrDefault().ProfileCommissionRate
                            }).ToList();
            return result;
        }

        public IList<Business.DataTransferObjects.Report.ProfileReport> GetProfileReportCurrentByCountry(int CountryID)
        {
            //int currentDay = DateTime.UtcNow.Day;
            var result = (from w in context.Profiles
                          where w.SubscriptionTypeID != 4 && w.Addresses.Any(a => a.City.CountryID == CountryID)
                          select
                            new Business.DataTransferObjects.Report.ProfileReport
                            {
                                ProfileFullname = w.ProfileFullname,
                                ProfileID = w.ProfileID,
                                ProfileView = w.ProfileView,
                                AlbumView = w.Albums.Sum(av => av.AlbumView),
                                AlbumComments = w.Albums.Sum(av => av.Contents.Sum(cv => cv.Comments.Count)),
                                AlbumLikes = w.Albums.Sum(av => av.Contents.Sum(cv => cv.ContentLike)),
                                Subscription = w.Connections.Where(sv => sv.RequestTypeID == 1).Count(),
                                StayOnConnection = w.Connections.Where(sv => sv.RequestTypeID == 3).Count(),
                                OverAllRevenue = w.ProfileReports.Where(pp => pp.ReportTypeID == 1 || pp.ReportTypeID == 2).Sum(pps => pps.ProfileReportRevenue),
                                OverAllView = w.ProfileReports.Where(pp => pp.ReportTypeID == 1 || pp.ReportTypeID == 2).Sum(pps => pps.ProfileReportActivityCount),
                                OverAllEngagement = w.ProfileReports.Where(pp => pp.ReportTypeID == 3 || pp.ReportTypeID == 4).Sum(pps => pps.ProfileReportActivityCount),
                                OverAllSubscription = w.ProfileReports.Where(pp => pp.ReportTypeID == 5 || pp.ReportTypeID == 6).Sum(pps => pps.ProfileReportActivityCount),
                                ConnectedUserShare = w.ProfileCommissions.Where(pc => pc.UserRoleID != 1).Sum(spc => spc.ProfileCommissionRate),
                                UserCommission = w.ProfileCommissions.Where(pc => pc.User.CountryID == CountryID && pc.UserRoleID == 6).FirstOrDefault().ProfileCommissionRate
                            }).ToList();
            return result;
        }



        public Business.DataTransferObjects.Web.AdminMiniProfile GetMiniProfile(int ProfileID)
        {
            return (from w in context.Profiles.Include("Addresses").Include("Industries") where w.ProfileID == ProfileID select new Business.DataTransferObjects.Web.AdminMiniProfile { ProfileTypeName = w.ProfileType.ProfileTypeName, CountryName = w.Addresses.FirstOrDefault().City.Country.CountryName, IndustryName = w.Industries.FirstOrDefault().IndustryName, ProfileFullname = w.ProfileFullname, ProfileID = w.ProfileID, ProfileImage = w.ProfileImage }).FirstOrDefault();
        }

        public Business.DataTransferObjects.Web.LowAppProfileModel GetLowAppProfile(string ProfileURL)
        {
            return (from w in context.Profiles where w.ProfileURL == ProfileURL select new Business.DataTransferObjects.Web.LowAppProfileModel { ProfileFullname = w.ProfileFullname, ProfileID = w.ProfileID, ProfileImage = w.ProfileImage }).FirstOrDefault();
        }

        public Profile GetProfile(string ProfileEmail)
        {
            return (from w in context.Profiles where w.ProfileEmail == ProfileEmail select w).FirstOrDefault();
        }
        //V2
        public Business.DataTransferObjects.Mobile.V2.LoginAttempt getLoginAttempt(string ProfileEmail, int? ProfileID)
        {
            return (from w in context.Profiles where w.ProfileEmail == ProfileEmail || w.ProfileID == ProfileID select new Business.DataTransferObjects.Mobile.V2.LoginAttempt { ProfileEmail = w.ProfileEmail, ProfileID = w.ProfileID }).FirstOrDefault();
        }

        public int[] GetAdministrationProfiles(int CountryID)
        {
            return (from w in context.Profiles where w.Addresses.Any(m => m.City.CountryID == CountryID) select w.ProfileID).ToArray();
        }

        public IEnumerable<Profile> GetProfiles(string ProfileEmail)
        {
            return (from w in context.Profiles where w.UserProfiles.Any(m => m.User.UserEmail == ProfileEmail) select w).ToList();
        }

        //public Profile GetUserProfile(string ProfileEmail)
        //{
        //    return (from w in context.Profiles where w.ProfileEmail == ProfileEmail && w.UserProfiles.Any(m=> m.User.UserEmail == ProfileEmail) select w).FirstOrDefault();
        //}

        public IEnumerable<Profile> getListed(int ProfileID)
        {
            int[] arrayTags = new int[1];
            return (from w in context.Profiles where w.Connections.Any(e => e.Profile1.ProfileTags.Any(m => arrayTags.Contains(m.TagID)) && e.Profile.ProfileID == ProfileID) orderby w.ProfileID descending select w).ToList();
        }

        //Lists
        public List<Profile> GetProfilesAll()
        {
            return (from w in context.Profiles orderby w.ProfileID descending select w).ToList();
        }
        public List<Profile> GetProfilesAll(bool ProfileStatus)
        {
            return (from w in context.Profiles where w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).ToList();
        }
        //List Pagings
        public List<Profile> GetProfilesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Profile>();
            if (filter != null)
            {
                xList = (from w in context.Profiles orderby w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Profiles orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Profile> GetProfilesAll(bool ProfileStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Profile>();
            if (filter != null)
            {
                xList = (from w in context.Profiles where w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Profiles where w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Profile> GetProfilesAllAsQueryable()
        {
            return (from w in context.Profiles orderby w.ProfileID descending select w).AsQueryable();
        }

        public IQueryable<Profile> GetProfilesAllAsQueryable(int CountryID)
        {
            return (from w in context.Profiles where w.Addresses.Any(a => a.City.CountryID == CountryID) orderby w.ProfileFullname ascending select w).AsQueryable();
        }

        public IQueryable<Profile> GetProfilesAllAsQueryable(bool ProfileStatus)
        {
            return (from w in context.Profiles where w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Profile> GetProfilesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Profile> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Profiles orderby w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Profiles.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Profiles orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Profiles.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Profile> GetProfilesAllAsQueryable(bool ProfileStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Profile> xList;
            if (filter != null)
            {
                xList = (from w in context.Profiles where w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Profiles where w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Profile> GetProfilesByIndustryID(int IndustryID, bool ProfileStatus)
        {
            return (from w in context.Profiles where w.Industries.Any(m => m.IndustryID == IndustryID) && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).ToList();
        }
        public List<Profile> GetProfilesByIndustryID(int IndustryID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.Industries.Any(m => m.IndustryID == IndustryID) orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Profile> GetProfilesByIndustryID(int IndustryID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.Industries.Any(m => m.IndustryID == IndustryID) && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Profile> GetProfilesByProfileTypeID(int ProfileTypeID, bool ProfileStatus)
        {
            return (from w in context.Profiles where w.ProfileTypeID == ProfileTypeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).ToList();
        }
        public List<Profile> GetProfilesByProfileTypeID(int ProfileTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.ProfileTypeID == ProfileTypeID orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Profile> GetProfilesByProfileTypeID(int ProfileTypeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.ProfileTypeID == ProfileTypeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Profile> GetProfilesByThemeID(int ThemeID, bool ProfileStatus)
        {
            return (from w in context.Profiles where w.ThemeID == ThemeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).ToList();
        }
        public List<Profile> GetProfilesByThemeID(int ThemeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.ThemeID == ThemeID orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Profile> GetProfilesByThemeID(int ThemeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.ThemeID == ThemeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Profile> GetProfilesBySubscriptionTypeID(int SubscriptionTypeID, bool ProfileStatus)
        {
            return (from w in context.Profiles where w.SubscriptionTypeID == SubscriptionTypeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).ToList();
        }
        public List<Profile> GetProfilesBySubscriptionTypeID(int SubscriptionTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.SubscriptionTypeID == SubscriptionTypeID orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Profile> GetProfilesBySubscriptionTypeID(int SubscriptionTypeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.SubscriptionTypeID == SubscriptionTypeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Profile> GetProfilesByIndustryIDAsQueryable(int IndustryID)
        {
            return (from w in context.Profiles where w.Industries.Any(m => m.IndustryID == IndustryID) orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByIndustryIDAsQueryable(int IndustryID, bool ProfileStatus)
        {
            return (from w in context.Profiles where w.Industries.Any(m => m.IndustryID == IndustryID) && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByIndustryIDAsQueryable(int IndustryID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.Industries.Any(m => m.IndustryID == IndustryID) orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByIndustryIDAsQueryable(int IndustryID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.Industries.Any(m => m.IndustryID == IndustryID) && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByProfileTypeIDAsQueryable(int ProfileTypeID)
        {
            return (from w in context.Profiles where w.ProfileTypeID == ProfileTypeID orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByProfileTypeIDAsQueryable(int ProfileTypeID, bool ProfileStatus)
        {
            return (from w in context.Profiles where w.ProfileTypeID == ProfileTypeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByProfileTypeIDAsQueryable(int ProfileTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.ProfileTypeID == ProfileTypeID orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByProfileTypeIDAsQueryable(int ProfileTypeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.ProfileTypeID == ProfileTypeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByThemeIDAsQueryable(int ThemeID)
        {
            return (from w in context.Profiles where w.ThemeID == ThemeID orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByThemeIDAsQueryable(int ThemeID, bool ProfileStatus)
        {
            return (from w in context.Profiles where w.ThemeID == ThemeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByThemeIDAsQueryable(int ThemeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.ThemeID == ThemeID orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesByThemeIDAsQueryable(int ThemeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.ThemeID == ThemeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesBySubscriptionTypeIDAsQueryable(int SubscriptionTypeID)
        {
            return (from w in context.Profiles where w.SubscriptionTypeID == SubscriptionTypeID orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesBySubscriptionTypeIDAsQueryable(int SubscriptionTypeID, bool ProfileStatus)
        {
            return (from w in context.Profiles where w.SubscriptionTypeID == SubscriptionTypeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesBySubscriptionTypeIDAsQueryable(int SubscriptionTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.SubscriptionTypeID == SubscriptionTypeID orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Profile> GetProfilesBySubscriptionTypeIDAsQueryable(int SubscriptionTypeID, bool ProfileStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Profiles where w.SubscriptionTypeID == SubscriptionTypeID && w.ProfileStatus == ProfileStatus orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}