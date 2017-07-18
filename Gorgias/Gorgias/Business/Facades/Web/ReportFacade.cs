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
        public FullProfileReport getCurrentProfileReport(int paramID, bool isCountry)
        {
            DataTransferObjects.RevenueDTO resultRevenue;
            try
            {
                resultRevenue = new BusinessLayer.Facades.RevenueFacade().GetRevenueCurrentReport();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            IList<DataTransferObjects.Report.ProfileReport> result;

            if (isCountry)
            {
                result = new BusinessLayer.Facades.ProfileFacade().GetProfileReportCurrentByCountry(paramID);
            } else
            {
                result = new BusinessLayer.Facades.ProfileFacade().GetProfileReportCurrent(paramID);
            }

            int actualView = 0;
            int actualSubscription = 0;
            int actualEngagement = 0;
            double? overallRevenue = 0;
            int overallView = 0;
            int overallSubscription = 0;
            int overallEngagement = 0;

            int overallTotalView = 0;
            int overallTotalSubscription = 0;
            int overallTotalEngagement = 0;

            foreach (DataTransferObjects.Report.ProfileReport obj in result)
            {
                var previousResults = new BusinessLayer.Facades.ProfileReportFacade().GetProfileReportsByProfileID(obj.ProfileID, resultRevenue.RevenueID);
                //First time or no ;)
                if (previousResults.Count() != 0)
                {
                    obj.ProfileView = compareValues(setNullableInt(obj.ProfileView), previousResults.Where(m => m.ReportTypeID == 1).First().ProfileReportActivityCount);
                    obj.AlbumView = compareValues(setNullableInt(obj.AlbumView), previousResults.Where(m => m.ReportTypeID == 2).First().ProfileReportActivityCount);
                    obj.AlbumComments = compareValues(setNullableInt(obj.AlbumComments), previousResults.Where(m => m.ReportTypeID == 3).First().ProfileReportActivityCount);
                    obj.AlbumLikes = compareValues(setNullableInt(obj.AlbumLikes), previousResults.Where(m => m.ReportTypeID == 4).First().ProfileReportActivityCount);
                    obj.StayOnConnection = compareValues(setNullableInt(obj.StayOnConnection), previousResults.Where(m => m.ReportTypeID == 5).First().ProfileReportActivityCount);
                    obj.Subscription = compareValues(setNullableInt(obj.Subscription), previousResults.Where(m => m.ReportTypeID == 6).First().ProfileReportActivityCount);

                    actualView = actualView + setNullableInt(obj.TotalView);
                    actualSubscription = actualSubscription + setNullableInt(obj.TotalSubscription);
                    actualEngagement = actualEngagement + setNullableInt(obj.TotalEngagement);

                    overallRevenue = overallRevenue + obj.OverAllRevenue;
                    overallView = overallView + setNullableInt(obj.OverAllView);
                    overallSubscription = overallSubscription + setNullableInt(obj.OverAllSubscription);
                    overallEngagement = overallEngagement + setNullableInt(obj.OverAllEngagement);

                    overallTotalView = overallTotalView + setNullableInt(obj.OverAllTotalView);
                    overallTotalSubscription = overallTotalSubscription + setNullableInt(obj.OverAllTotalSubscription);
                    overallTotalEngagement = overallTotalEngagement + setNullableInt(obj.OverAllTotalEngagement);
                }
            }

            FullProfileReport resultFullProfileReport = new FullProfileReport();
            resultFullProfileReport.ProfileReports = result;
            resultFullProfileReport.TotalView = resultRevenue.RevenueTotalViews;
            resultFullProfileReport.RevenueAmount = resultRevenue.RevenueAmount;
            resultFullProfileReport.ActualEngagement = actualEngagement;
            resultFullProfileReport.ActualSubscription = actualSubscription;
            resultFullProfileReport.ActualView = actualView;

            resultFullProfileReport.OverAllRevenue = setNullableDouble(overallRevenue);
            resultFullProfileReport.OverAllEngagement = overallEngagement;
            resultFullProfileReport.OverAllSubscription = overallSubscription;
            resultFullProfileReport.OverAllView = overallView;

            resultFullProfileReport.OverAllTotalEngagement = overallTotalEngagement;
            resultFullProfileReport.OverAllTotalSubscription = overallTotalSubscription;
            resultFullProfileReport.OverAllTotalView = overallTotalView;

            return resultFullProfileReport;
        }

        public FullProfileReport getCurrentProfileReport(int UserID)
        {
            DataTransferObjects.RevenueDTO resultRevenue;
            try
            {
                resultRevenue = new BusinessLayer.Facades.RevenueFacade().GetRevenueCurrentReport();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            IList<DataTransferObjects.Report.ProfileReport> result = new BusinessLayer.Facades.ProfileFacade().GetProfileReportCurrent(UserID);

            int actualView = 0;
            int actualSubscription = 0;
            int actualEngagement = 0;
            double? overallRevenue = 0;
            int overallView = 0;
            int overallSubscription = 0;
            int overallEngagement = 0;

            int overallTotalView = 0;
            int overallTotalSubscription = 0;
            int overallTotalEngagement = 0;

            foreach (DataTransferObjects.Report.ProfileReport obj in result)
            {
                var previousResults = new BusinessLayer.Facades.ProfileReportFacade().GetProfileReportsByProfileID(obj.ProfileID, resultRevenue.RevenueID);
                //First time or no ;)
                if (previousResults.Count() != 0)
                {
                    obj.ProfileView = compareValues(setNullableInt(obj.ProfileView), previousResults.Where(m => m.ReportTypeID == 1).First().ProfileReportActivityCount);
                    obj.AlbumView = compareValues(setNullableInt(obj.AlbumView), previousResults.Where(m => m.ReportTypeID == 2).First().ProfileReportActivityCount);
                    obj.AlbumComments = compareValues(setNullableInt(obj.AlbumComments), previousResults.Where(m => m.ReportTypeID == 3).First().ProfileReportActivityCount);
                    obj.AlbumLikes = compareValues(setNullableInt(obj.AlbumLikes), previousResults.Where(m => m.ReportTypeID == 4).First().ProfileReportActivityCount);
                    obj.StayOnConnection = compareValues(setNullableInt(obj.StayOnConnection), previousResults.Where(m => m.ReportTypeID == 5).First().ProfileReportActivityCount);
                    obj.Subscription = compareValues(setNullableInt(obj.Subscription), previousResults.Where(m => m.ReportTypeID == 6).First().ProfileReportActivityCount);

                    actualView = actualView + setNullableInt(obj.TotalView);
                    actualSubscription = actualSubscription + setNullableInt(obj.TotalSubscription);
                    actualEngagement = actualEngagement + setNullableInt(obj.TotalEngagement);

                    overallRevenue = overallRevenue + obj.OverAllRevenue;
                    overallView = overallView + setNullableInt(obj.OverAllView);
                    overallSubscription = overallSubscription + setNullableInt(obj.OverAllSubscription);
                    overallEngagement = overallEngagement + setNullableInt(obj.OverAllEngagement);

                    overallTotalView = overallTotalView + setNullableInt(obj.OverAllTotalView);
                    overallTotalSubscription = overallTotalSubscription + setNullableInt(obj.OverAllTotalSubscription);
                    overallTotalEngagement = overallTotalEngagement + setNullableInt(obj.OverAllTotalEngagement);
                }
            }

            FullProfileReport resultFullProfileReport = new FullProfileReport();
            resultFullProfileReport.ProfileReports = result;
            resultFullProfileReport.TotalView = resultRevenue.RevenueTotalViews;
            resultFullProfileReport.RevenueAmount = resultRevenue.RevenueAmount;
            resultFullProfileReport.ActualEngagement = actualEngagement;
            resultFullProfileReport.ActualSubscription = actualSubscription;
            resultFullProfileReport.ActualView = actualView;

            resultFullProfileReport.OverAllRevenue = setNullableDouble(overallRevenue);
            resultFullProfileReport.OverAllEngagement = overallEngagement;
            resultFullProfileReport.OverAllSubscription = overallSubscription;
            resultFullProfileReport.OverAllView = overallView;

            resultFullProfileReport.OverAllTotalEngagement = overallTotalEngagement;
            resultFullProfileReport.OverAllTotalSubscription = overallTotalSubscription;
            resultFullProfileReport.OverAllTotalView = overallTotalView;

            return resultFullProfileReport;
        }

        public IList<DataTransferObjects.Report.ProfileReport> getCurrentProfileReportByCountry(int CountryID)
        {
            DataTransferObjects.RevenueDTO resultRevenue;
            try
            {
                resultRevenue = new BusinessLayer.Facades.RevenueFacade().GetRevenueCurrentReport();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            IList<DataTransferObjects.Report.ProfileReport> result = new BusinessLayer.Facades.ProfileFacade().GetProfileReportCurrentByCountry(CountryID);

            foreach (DataTransferObjects.Report.ProfileReport obj in result)
            {
                var previousResults = new BusinessLayer.Facades.ProfileReportFacade().GetProfileReportsByProfileID(obj.ProfileID);
                //First time or no ;)
                if (previousResults.Count() != 0)
                {
                    obj.ProfileView = compareValues(setNullableInt(obj.ProfileView), previousResults.Where(m => m.ReportTypeID == 1).First().ProfileReportActivityCount);
                    obj.AlbumView = compareValues(setNullableInt(obj.AlbumView), previousResults.Where(m => m.ReportTypeID == 2).First().ProfileReportActivityCount);
                    obj.AlbumComments = compareValues(setNullableInt(obj.AlbumComments), previousResults.Where(m => m.ReportTypeID == 3).First().ProfileReportActivityCount);
                    obj.AlbumLikes = compareValues(setNullableInt(obj.AlbumLikes), previousResults.Where(m => m.ReportTypeID == 4).First().ProfileReportActivityCount);
                    obj.StayOnConnection = compareValues(setNullableInt(obj.StayOnConnection), previousResults.Where(m => m.ReportTypeID == 5).First().ProfileReportActivityCount);
                    obj.Subscription = compareValues(setNullableInt(obj.Subscription), previousResults.Where(m => m.ReportTypeID == 6).First().ProfileReportActivityCount);
                }
            }

            return result;
        }

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
                    new BusinessLayer.Facades.ProfileReportFacade().Insert(new DataTransferObjects.ProfileReportDTO { ProfileID = obj.ProfileID, ReportTypeID = 1, ProfileReportActivityCount = compareValues(obj.ProfileView.Value, previousResults.Where(m => m.ReportTypeID == 1).First().ProfileReportActivityCount), RevenueID = resultRevenue.RevenueID, ProfileReportRevenue = resultRevenue.RevenueAmount });
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

        private double setNullableDouble(double? param)
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
            if ((newParam == 0 && oldParam == 0) || newParam == 0)
            {
                return 0;
            }
            if (newParam != 0 && oldParam == 0)
            {
                return newParam;
            }
            else
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