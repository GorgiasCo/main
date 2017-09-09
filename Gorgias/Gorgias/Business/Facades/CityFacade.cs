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
    public class CityFacade
    {
        //V2 Begin ;)
        public IQueryable<CityDTO> getCitiesByLanguage(int CountryID, string languageCode)
        {
            return DataLayer.DataLayerFacade.CityRepository().GetCitiesAllAsQueryable(CountryID,languageCode);
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.CityMobileModel> getCities(int CountryID, string languageCode)
        {
            return DataLayer.DataLayerFacade.CityRepository().GetCitiesAsQueryable(CountryID,languageCode);
        }
        //V2 End ;)
        public CityDTO GetCity(int CityID)
        {
            CityDTO result = Mapper.Map<CityDTO>(DataLayer.DataLayerFacade.CityRepository().GetCity(CityID));
            return result;
        }

        public CityDTO GetCity(string CityName)
        {
            CityDTO result = Mapper.Map<CityDTO>(DataLayer.DataLayerFacade.CityRepository().GetCity(CityName));
            return result;
        }

        public DTResult<CityDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.CityRepository().GetCitiesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.CityName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<City>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<CityDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<CityDTO> result = new DTResult<CityDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<CityDTO> GetCities(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.CityRepository().GetCitiesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<City>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<CityDTO> result = new PaginationSet<CityDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<CityDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<CityDTO> GetCities()
        {
            var basequery = Mapper.Map<List<CityDTO>>(DataLayer.DataLayerFacade.CityRepository().GetCitiesAllAsQueryable().Where(m=> m.CityParentID == null));
            return basequery.ToList();
        }


        public DTResult<CityDTO> FilterResultByCountryID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int CountryID)
        {
            var basequery = DataLayer.DataLayerFacade.CityRepository().GetCitiesAllAsQueryable().Where(m => m.CountryID == CountryID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.CityName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<City>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<CityDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<CityDTO> result = new DTResult<CityDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<CityDTO> GetCitiesByCountryID(int CountryID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.CityRepository().GetCitiesByCountryIDAsQueryable(CountryID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<City>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<CityDTO> result = new PaginationSet<CityDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<CityDTO>>(queryList.ToList())
            };

            return result;
        }

        public CityDTO Insert(CityDTO objCity)
        {
            CityDTO result = Mapper.Map<CityDTO>(DataLayer.DataLayerFacade.CityRepository().Insert(objCity.CityName, objCity.CityStatus, objCity.CountryID, objCity.CityUpdateDate, objCity.CityLanguageCode, objCity.CityParentID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new CityDTO();
            }
        }

        public bool Delete(int CityID)
        {
            bool result = DataLayer.DataLayerFacade.CityRepository().Delete(CityID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int CityID, CityDTO objCity)
        {
            bool result = DataLayer.DataLayerFacade.CityRepository().Update(objCity.CityID, objCity.CityName, objCity.CityStatus, objCity.CountryID, objCity.CityUpdateDate, objCity.CityLanguageCode, objCity.CityParentID);
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