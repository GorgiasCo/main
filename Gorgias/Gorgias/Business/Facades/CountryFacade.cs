using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Gorgias.DataLayer.Repository;
using Gorgias.Business.DataTransferObjects;
using Gorgias.Infrastruture.Core;
using Gorgias.Business.DataTransferObjects.Helper;
using EntityFramework.Extensions;

namespace Gorgias.BusinessLayer.Facades
{   
    public class CountryFacade
    {                
        public CountryDTO GetCountry(int CountryID)
        {
            CountryDTO result = Mapper.Map<CountryDTO>(DataLayer.DataLayerFacade.CountryRepository().GetCountry(CountryID));             
            return result;
        }

        public DTResult<CountryDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.CountryRepository().GetCountriesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.CountryName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Country>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<CountryDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<CountryDTO> result = new DTResult<CountryDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<CountryDTO> GetCountries(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.CountryRepository().GetCountriesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Country>(page, pagesize, basequery).Future();
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
        
        public List<CountryDTO> GetCountries()
        {           
            var basequery = Mapper.Map <List<CountryDTO>>(DataLayer.DataLayerFacade.CountryRepository().GetCountriesAllAsQueryable());
            return basequery.ToList();
        }

        

        public CountryDTO Insert(CountryDTO objCountry)
        {            
            CountryDTO result = Mapper.Map<CountryDTO>(DataLayer.DataLayerFacade.CountryRepository().Insert(objCountry.CountryName, objCountry.CountryShortName, objCountry.CountryStatus, objCountry.CountryPhoneCode, objCountry.CountryImage, objCountry.CountryDescription));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new CountryDTO();
            }
        }
               
        public bool Delete(int CountryID)
        {            
            bool result = DataLayer.DataLayerFacade.CountryRepository().Delete(CountryID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int CountryID, CountryDTO objCountry)
        {            
            bool result = DataLayer.DataLayerFacade.CountryRepository().Update(objCountry.CountryID, objCountry.CountryName, objCountry.CountryShortName, objCountry.CountryStatus, objCountry.CountryPhoneCode, objCountry.CountryImage, objCountry.CountryDescription);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }
    }
}