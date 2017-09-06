using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IProfileActivityRepository
    {
        ProfileActivity Insert(int ProfileID, int AlbumID, int ActivityTypeID, int ProfileActivityCount, DateTime ProfileActivityDateTime);
        bool Update(int ProfileID, int AlbumID, int ActivityTypeID, int ProfileActivityCount, DateTime ProfileActivityDateTime);
        bool Delete(int ProfileID, int AlbumID);

        ProfileActivity GetProfileActivity(int ProfileID, int AlbumID);

        //List
        List<ProfileActivity> GetProfileActivitiesAll();
        List<ProfileActivity> GetProfileActivitiesAll(int page = 1, int pageSize = 7, string filter = null);

        List<ProfileActivity> GetProfileActivitiesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
        List<ProfileActivity> GetProfileActivitiesByAlbumID(int AlbumID, int page = 1, int pageSize = 7, string filter = null);
        List<ProfileActivity> GetProfileActivitiesByActivityTypeID(int ActivityTypeID, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<ProfileActivity> GetProfileActivitiesAllAsQueryable();
        IQueryable<ProfileActivity> GetProfileActivitiesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ProfileActivity> GetProfileActivitiesByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileActivity> GetProfileActivitiesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ProfileActivity> GetProfileActivitiesByAlbumIDAsQueryable(int AlbumID);
        IQueryable<ProfileActivity> GetProfileActivitiesByAlbumIDAsQueryable(int AlbumID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ProfileActivity> GetProfileActivitiesByActivityTypeIDAsQueryable(int ActivityTypeID);
        IQueryable<ProfileActivity> GetProfileActivitiesByActivityTypeIDAsQueryable(int ActivityTypeID, int page = 1, int pageSize = 7, string filter = null);
    }
}


