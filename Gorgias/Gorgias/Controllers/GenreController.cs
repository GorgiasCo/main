using Gorgias.Infrastruture.Core;
using Gorgias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gorgias.Infrastruture.Validators;
using Gorgias.Infrastruture.Filters;
using Gorgias.Infrastruture.EntityFramework;
using EntityFramework.Extensions;
using Gorgias.DataLayer.Repository.SQL;
using AutoMapper;
using Gorgias.DataLayer.Repository;
using System.Data.SqlClient;

namespace Gorgias.Controllers
{
    [RoutePrefix("api/genres")]
    public class GenreController : ApiControllerBase
    {
        [AllowAnonymous]
        [Route("GetValue", Name = "GetGenre")]
        public IEnumerable<string> Get()
        {
            GorgiasEntities en = new GorgiasEntities();

            return new string[] { "value1", "value2", "value333", "value313", "Thanks Allah", "value333", "value313", "Thanks Allah", "value333", "value313", "Thanks Allah", "value333", "value313", "Thanks Allah", "value333", "value313", "Thanks Allah", "value333", "value313", "Thanks Allah", "value333", "value313", "Thanks Allah", "value333", "value313", "Thanks Allah" };
        }

        [AllowAnonymous]
        [Route("GetCountry/{currentPage}/{currentPageSize}", Name = "GetCountry")]
        public IEnumerable<Country> GetCountry(int currentPage, int currentPageSize)
        {
            GorgiasEntities en = new GorgiasEntities();
            //var alist = en.MultipleResults("[dbo].[thanksAllah]").With<Country>()
            //    .With<int>()
            //    .Execute();

            //var getCountries = en.MultipleResults("[dbo].[getCountries]").With<vCountry>()
            //    .With<int>()
            //    .Execute();
            int total = (from x in en.Countries select x).Count();
            var list = (from x in en.Countries select x).OrderBy(m => m.CountryID).Skip(currentPage * currentPageSize)
            .Take(currentPageSize)
            .ToList();
            //return (IEnumerable<Country>)getCountries[0];
            return list;

        }

        //[AllowAnonymous]
        //[Route("GetCountry/tune/{currentPage}/{currentPageSize}", Name = "GetCountryTune")]
        //public IEnumerable<vCountry> GetTuneCountry(int currentPage, int currentPageSize)
        //{
        //    GorgiasEntities en = new GorgiasEntities();
        //    //var alist = en.MultipleResults("[dbo].[thanksAllah]").With<Country>()
        //    //    .With<int>()
        //    //    .Execute();

        //    //var getCountries = en.MultipleResults("[dbo].[getCountries]").With<vCountry>()
        //    //    .With<int>()
        //    //    .Execute();

        //    //IDictionary<string, string> param = new Dictionary<string, string>();
        //    //param.Add("@CountryID", "1");
        //    //param.Add("@CountryName", "Yasser");

        //    IList<SqlParameter> param = new List<SqlParameter>();
        //    SqlParameter param1 = new SqlParameter();
        //    param1.DbType = System.Data.DbType.Int16;
        //    param1.Value = currentPage;
        //    param1.ParameterName = "@CountryID";
        //    param1.Direction = System.Data.ParameterDirection.Input;
        //    param.Add(param1);



        //    var getCountries = new CountryRepository().getStoredProcedure<vCountry,int> ("[dbo].[getCountries]",param);
        //        //en.MultipleResults("[dbo].[getCountries]").With<vCountry>()
        //        //.With<int>()
        //        //.Execute();

        //    return (IEnumerable<vCountry>)getCountries[0];            
        //}

        [AllowAnonymous]
        [Route("GetCountry/future/{currentPage}/{currentPageSize}", Name = "GetCountryFuture")]
        public IEnumerable<Country> GetFutureCountry(int currentPage, int currentPageSize)
        {
            GorgiasEntities en = new GorgiasEntities();

            var baseQuery = (from x in en.Countries select x).OrderBy(m => m.CountryID);

            var totalQuary = baseQuery.FutureCount();
            var list = baseQuery.Skip(currentPage * currentPageSize).Take(currentPageSize).Future();

            int total = totalQuary.Value;

            return list.ToList();
        }

        //[AllowAnonymous]
        //[Route("GetCountry/common/{currentPage}/{currentPageSize}", Name = "GetCountryCommon")]
        //public HttpResponseMessage GetCommonCountry(HttpRequestMessage request,int currentPage, int currentPageSize)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        response = request.CreateResponse<PaginationSet<CountryDTO>>(HttpStatusCode.OK, BusinessLayer.Facades.CountryFacade.getCountries(currentPage,currentPageSize));               
        //        return response;
        //    });
        //}

        [AllowAnonymous]
        [Route("GetValues/{ID}", Name = "GetGenres")]
        public IEnumerable<string> Get(int ID)
        {
            if (ID > 7)
            {
                return new string[] { "value1", "value2" };
            }
            else {
                return new string[] { "value333", "value313", "Thanks Allah" };
            }
            
        }

        [AllowAnonymous]
        [Route("add", Name = "AddGenre")]
        public HttpResponseMessage Post(HttpRequestMessage request, Genre value)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else {
                    String name = value.Name;
                    response = request.CreateResponse<String>(HttpStatusCode.NotFound, name);
                }
                return response;
            });
            
        }
    }
}
