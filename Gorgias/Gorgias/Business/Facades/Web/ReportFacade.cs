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
            IEnumerable <DataTransferObjects.Report.ProfileReport> result = new BusinessLayer.Facades.ProfileFacade().GetProfileReportCurrent();

            foreach(DataTransferObjects.Report.ProfileReport obj in result)
            {
                var previousResults = new BusinessLayer.Facades.ProfileReportFacade().GetProfileReportsByProfileID(obj.ProfileID);
            }

            return result;
        }

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
            } else
            {
                int? CountryID = new BusinessLayer.Facades.UserFacade().GetUser(UserID).CountryID;
                if (CountryID.HasValue)
                {
                    ProfileID = string.Join(",", new BusinessLayer.Facades.ProfileFacade().GetProfilesAdministration(CountryID.Value).Select(p => p.ToString()));
                } else
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