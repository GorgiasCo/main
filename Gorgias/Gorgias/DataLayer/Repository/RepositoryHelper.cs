using Gorgias.Infrastruture.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;

namespace Gorgias.DataLayer.Repository
{
    public class RepositoryHelper
    {
        public static IQueryable<T> Pagination<T>(int page, int pagesize, IQueryable query) {            
            return (IQueryable<T>)query.Skip(pagesize * (page - 1)).Take(pagesize);
        }

        public static IQueryable<T> PaginationDatatables<T>(int page, int pagesize, IQueryable query)
        {
            return (IQueryable<T>)query.Skip(page).Take(pagesize);
        }

        public static List<IEnumerable> getStoredProcedure<T, Y>(string name, IList<SqlParameter> param = null) where T : class
        {
            var results = new GorgiasEntities().MultipleResults(name, param).With<T>().With<Y>().Execute();
            return results;
        }
    }
}