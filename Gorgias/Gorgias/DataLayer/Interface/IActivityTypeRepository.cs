using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IActivityTypeRepository
    {
        ActivityType Insert(String ActivityTypeName, String ActivityTypeLanguageCode, Boolean ActivityTypeStatus, int? ActivityTypeParentID);
        bool Update(int ActivityTypeID, String ActivityTypeName, String ActivityTypeLanguageCode, Boolean ActivityTypeStatus, int? ActivityTypeParentID);
        bool Delete(int ActivityTypeID);

        ActivityType GetActivityType(int ActivityTypeID);

        //List
        List<ActivityType> GetActivityTypesAll();
        List<ActivityType> GetActivityTypesAll(bool ActivityTypeStatus);
        List<ActivityType> GetActivityTypesAll(int page = 1, int pageSize = 7, string filter = null);
        List<ActivityType> GetActivityTypesAll(bool ActivityTypeStatus, int page = 1, int pageSize = 7, string filter = null);

        List<ActivityType> GetActivityTypesByActivityTypeParentID(int ActivityTypeParentID, bool ActivityTypeStatus);
        List<ActivityType> GetActivityTypesByActivityTypeParentID(int ActivityTypeParentID, int page = 1, int pageSize = 7, string filter = null);
        List<ActivityType> GetActivityTypesByActivityTypeParentID(int ActivityTypeParentID, bool ActivityTypeStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<ActivityType> GetActivityTypesAllAsQueryable();
        IQueryable<Business.DataTransferObjects.ActivityTypeDTO> GetActivityTypesAllAsQueryable(string languageCode);
        IQueryable<ActivityType> GetActivityTypesAllAsQueryable(bool ActivityTypeStatus);
        //V2
        IQueryable<Business.DataTransferObjects.Mobile.V2.ActivityTypeMobileModel> GetActivityTypesFeltAsQueryable(string languageCode, int ActivityTypeParentID);
        //End V2
        IQueryable<ActivityType> GetActivityTypesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ActivityType> GetActivityTypesAllAsQueryable(bool ActivityTypeStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ActivityType> GetActivityTypesByActivityTypeParentIDAsQueryable(int ActivityTypeParentID);
        IQueryable<ActivityType> GetActivityTypesByActivityTypeParentIDAsQueryable(int ActivityTypeParentID, bool ActivityTypeStatus);
        IQueryable<ActivityType> GetActivityTypesByActivityTypeParentIDAsQueryable(int ActivityTypeParentID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ActivityType> GetActivityTypesByActivityTypeParentIDAsQueryable(int ActivityTypeParentID, bool ActivityTypeStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


