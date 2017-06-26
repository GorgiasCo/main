using AutoMapper;
using EntityFramework.Extensions;
using Gorgias.DataLayer.Repository;
using Gorgias.DataLayer.Repository.SQL;
using Gorgias.Infrastruture.Core;
using Gorgias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.BusinessLayer.Facades
{
    public class CountryFacadeI
    {
        public static PaginationSet<CountryDTO> getCountries(int page, int pagesize) {
            var basequery = new CountryRepository().GetCountriesAllAsQueryable();

            var queryList = RepositoryHelper.Pagination<Country>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<CountryDTO> result = new PaginationSet<CountryDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<CountryDTO>>(queryList.ToList())
            };

            return result;
        }

    }
}