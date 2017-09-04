using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IProfileReadingRepository
    {
        ProfileReading Insert(String ProfileReadingLanguageCode, DateTime ProfileReadingDatetime, int ProfileID);
        bool Update(int ProfileReadingID, String ProfileReadingLanguageCode, DateTime ProfileReadingDatetime, int ProfileID);
        bool Delete(int ProfileReadingID);
        bool DeleteByProfileID(int ProfileID);

        ProfileReading GetProfileReading(int ProfileReadingID);

        //List
        List<ProfileReading> GetProfileReadingsAll();
        List<ProfileReading> GetProfileReadingsAll(int page = 1, int pageSize = 7, string filter = null);

        List<ProfileReading> GetProfileReadingsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<ProfileReading> GetProfileReadingsAllAsQueryable();
        IQueryable<ProfileReading> GetProfileReadingsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ProfileReading> GetProfileReadingsByProfileIDAsQueryable(int ProfileID);
        IQueryable<ProfileReading> GetProfileReadingsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
    }
}


