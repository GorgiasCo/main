using Gorgias.Business.DataTransferObjects.Report;
using Gorgias.DataLayer.Repository.SQL.Web;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Gorgias.Business.Facades.Web
{
    public class ReportFacade
    {

        public IEnumerable<DataTransferObjects.Report.ProfileReport> getCurrentProfileReport()
        {
            DataTransferObjects.RevenueDTO resultRevenue;
            try
            {
                resultRevenue = new BusinessLayer.Facades.RevenueFacade().GetRevenueCurrent();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            IEnumerable<DataTransferObjects.Report.ProfileReport> result = new BusinessLayer.Facades.ProfileFacade().GetProfileReportCurrent();

            foreach (DataTransferObjects.Report.ProfileReport obj in result)
            {
                var previousResults = new BusinessLayer.Facades.ProfileReportFacade().GetProfileReportsByProfileID(obj.ProfileID);
                //First time or no ;)
                if (previousResults.Count() != 0)
                {
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 1, ProfileReportActivityCount = compareValues(obj.ProfileView.Value, previousResults.Where(m=> m.ReportTypeID == 1).First().ProfileReportActivityCount), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = resultRevenue.RevenueAmount });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 2, ProfileReportActivityCount = compareValues(setNullableInt(obj.AlbumView), previousResults.Where(m => m.ReportTypeID == 2).First().ProfileReportActivityCount), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = resultRevenue.RevenueAmount });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 3, ProfileReportActivityCount = compareValues(setNullableInt(obj.AlbumComments), previousResults.Where(m => m.ReportTypeID == 3).First().ProfileReportActivityCount), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = 0 });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 4, ProfileReportActivityCount = compareValues(setNullableInt(obj.AlbumLikes), previousResults.Where(m => m.ReportTypeID == 4).First().ProfileReportActivityCount), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = 0 });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 5, ProfileReportActivityCount = compareValues(setNullableInt(obj.StayOnConnection), previousResults.Where(m => m.ReportTypeID == 5).First().ProfileReportActivityCount), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = 0 });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 6, ProfileReportActivityCount = compareValues(setNullableInt(obj.Subscription), previousResults.Where(m => m.ReportTypeID == 6).First().ProfileReportActivityCount), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = 0 });
                }
                else
                {
                    //try
                    //{

                    //}
                    //catch (Exception ex)
                    //{
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 1, ProfileReportActivityCount = obj.ProfileView.Value, RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = resultRevenue.RevenueAmount });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 2, ProfileReportActivityCount = setNullableInt(obj.AlbumView), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = resultRevenue.RevenueAmount });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 3, ProfileReportActivityCount = setNullableInt(obj.AlbumComments), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = 0 });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 4, ProfileReportActivityCount = setNullableInt(obj.AlbumLikes), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = 0 });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 5, ProfileReportActivityCount = setNullableInt(obj.StayOnConnection), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = 0 });
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 6, ProfileReportActivityCount = setNullableInt(obj.Subscription), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = 0 });
                    //}
                }
            }

            return result;
        }

        //Helper Function
        private int setNullableInt(int? param)
        {
            if (param.HasValue)
            {
                return param.Value;
            }
            else
            {
                return 0;
            }
        }

        private int compareValues(int newParam, int oldParam)
        {
            if((newParam == 0 && oldParam == 0) || newParam == 0)
            {
                return 0;
            } if(newParam != 0 && oldParam == 0)
            {
                return newParam;
            } else
            {
                return newParam - oldParam;
            }
        }
        //End of Helper Function

        public IEnumerable<AlbumResult> AlbumResults(int UserID, int Availability, int OrderType)
        {
            int[] arrayProfileID = new BusinessLayer.Facades.UserProfileFacade().GetAdministrationUserProfileReport(UserID);
            string ProfileID = "";

            if (arrayProfileID.Count() > 0)
            {
                ProfileID = string.Join(",", arrayProfileID.Select(p => p.ToString()));
            }
            else
            {
                int? CountryID = new BusinessLayer.Facades.UserFacade().GetUser(UserID).CountryID;
                if (CountryID.HasValue)
                {
                    ProfileID = string.Join(",", new BusinessLayer.Facades.ProfileFacade().GetProfilesAdministration(CountryID.Value).Select(p => p.ToString()));
                }
                else
                {
                    throw new Exception("Not Found");
                }
            }

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.String;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramAvailability = new SqlParameter();
            paramAvailability.DbType = System.Data.DbType.Int32;
            paramAvailability.Value = Availability;
            paramAvailability.ParameterName = "@AvailabilityID";
            paramAvailability.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int32;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);
            param.Add(paramAvailability);
            param.Add(paramOrderType);

            var r = new WebRepository().getStoredProcedure<AlbumResult>("[dbo].[getReportAlbumsByProfileID]", param);
            return (IEnumerable<AlbumResult>)r[0];
        }

        public IEnumerable<AlbumInformation> AlbumInformation(int UserID, int Availability, int OrderType)
        {
            int[] arrayProfileID = new BusinessLayer.Facades.UserProfileFacade().GetAdministrationUserProfileReport(UserID);
            string ProfileID = "";

            if (arrayProfileID.Count() > 0)
            {
                ProfileID = string.Join(",", arrayProfileID.Select(p => p.ToString()));
            }
            else
            {
                int? CountryID = new BusinessLayer.Facades.UserFacade().GetUser(UserID).CountryID;
                if (CountryID.HasValue)
                {
                    ProfileID = string.Join(",", new BusinessLayer.Facades.ProfileFacade().GetProfilesAdministration(CountryID.Value).Select(p => p.ToString()));
                }
                else
                {
                    throw new Exception("Not Found");
                }
            }

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.String;
            paramProfileID.Value = ProfileID;
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramAvailability = new SqlParameter();
            paramAvailability.DbType = System.Data.DbType.Int32;
            paramAvailability.Value = Availability;
            paramAvailability.ParameterName = "@AvailabilityID";
            paramAvailability.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int32;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);
            param.Add(paramAvailability);
            param.Add(paramOrderType);

            var r = new WebRepository().getStoredProcedure<AlbumInformation>("[dbo].[getReportAlbumsInformationByProfileID]", param);
            return (IEnumerable<AlbumInformation>)r[0];
        }

        public IEnumerable<AlbumInformation> AlbumMomentsInformation(int UserID, int Availability, int OrderType)
        {

            int[] arrayProfileID = new BusinessLayer.Facades.UserProfileFacade().GetAdministrationUserProfileReport(UserID);
            string ProfileID = "";

            if (arrayProfileID.Count() > 0)
            {
                ProfileID = string.Join(",", arrayProfileID.Select(p => p.ToString()));
            }
            else
            {
                int? CountryID = new BusinessLayer.Facades.UserFacade().GetUser(UserID).CountryID;
                if (CountryID.HasValue)
                {
                    ProfileID = string.Join(",", new BusinessLayer.Facades.ProfileFacade().GetProfilesAdministration(CountryID.Value).Select(p => p.ToString()));
                }
                else
                {
                    throw new Exception("Not Found");
                }
            }

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.String;
            paramProfileID.Value = ProfileID.ToString();
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramAvailability = new SqlParameter();
            paramAvailability.DbType = System.Data.DbType.Int32;
            paramAvailability.Value = Availability;
            paramAvailability.ParameterName = "@AvailabilityID";
            paramAvailability.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int32;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);
            param.Add(paramAvailability);
            param.Add(paramOrderType);

            var r = new WebRepository().getStoredProcedure<AlbumInformation>("[dbo].[getReportAlbumMomentsInformationByProfileID]", param);
            return (IEnumerable<AlbumInformation>)r[0];
        }

        public IEnumerable<AlbumLike> AlbumLikes(int UserID, int Availability, int OrderType)
        {
            int[] arrayProfileID = new BusinessLayer.Facades.UserProfileFacade().GetAdministrationUserProfileReport(UserID);
            string ProfileID = "";

            if (arrayProfileID.Count() > 0)
            {
                ProfileID = string.Join(",", arrayProfileID.Select(p => p.ToString()));
            }
            else
            {
                int? CountryID = new BusinessLayer.Facades.UserFacade().GetUser(UserID).CountryID;
                if (CountryID.HasValue)
                {
                    ProfileID = string.Join(",", new BusinessLayer.Facades.ProfileFacade().GetProfilesAdministration(CountryID.Value).Select(p => p.ToString()));
                }
                else
                {
                    throw new Exception("Not Found");
                }
            }

            IList<SqlParameter> param = new List<SqlParameter>();

            SqlParameter paramProfileID = new SqlParameter();
            paramProfileID.DbType = System.Data.DbType.String;
            paramProfileID.Value = ProfileID.ToString();
            paramProfileID.ParameterName = "@ProfileID";
            paramProfileID.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramAvailability = new SqlParameter();
            paramAvailability.DbType = System.Data.DbType.Int32;
            paramAvailability.Value = Availability;
            paramAvailability.ParameterName = "@AvailabilityID";
            paramAvailability.Direction = System.Data.ParameterDirection.Input;

            SqlParameter paramOrderType = new SqlParameter();
            paramOrderType.DbType = System.Data.DbType.Int32;
            paramOrderType.Value = OrderType;
            paramOrderType.ParameterName = "@OrderType";
            paramOrderType.Direction = System.Data.ParameterDirection.Input;

            param.Add(paramProfileID);
            param.Add(paramAvailability);
            param.Add(paramOrderType);

            var r = new WebRepository().getStoredProcedure<AlbumLike>("[dbo].[getReportAlbumLikeByProfileID]", param);
            return (IEnumerable<AlbumLike>)r[0];
        }

    }
}