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
    public class ProfileReadingFacade
    {
        //V2 Begin
        public bool updateProfileReadings(Business.DataTransferObjects.Mobile.V2.ProfileReadingMobileModel profileReadings)
        {
            DataLayer.DataLayerFacade.ProfileReadingRepository().DeleteByProfileID(profileReadings.ProfileID);
            foreach(string obj in profileReadings.ProfileReadings)
            {
                DataLayer.DataLayerFacade.ProfileReadingRepository().Insert(obj, DateTime.UtcNow, profileReadings.ProfileID);
            }
            return true;
        }
        //V2 End
        public ProfileReadingDTO GetProfileReading(int ProfileReadingID)
        {
            ProfileReadingDTO result = Mapper.Map<ProfileReadingDTO>(DataLayer.DataLayerFacade.ProfileReadingRepository().GetProfileReading(ProfileReadingID));
            return result;
        }

        public DTResult<ProfileReadingDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileReadingRepository().GetProfileReadingsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileReadingLanguageCode.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileReading>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileReadingDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileReadingDTO> result = new DTResult<ProfileReadingDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ProfileReadingDTO> GetProfileReadings(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileReadingRepository().GetProfileReadingsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileReading>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileReadingDTO> result = new PaginationSet<ProfileReadingDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileReadingDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ProfileReadingDTO> GetProfileReadings()
        {
            var basequery = Mapper.Map<List<ProfileReadingDTO>>(DataLayer.DataLayerFacade.ProfileReadingRepository().GetProfileReadingsAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ProfileReadingDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileReadingRepository().GetProfileReadingsAllAsQueryable().Where(m => m.ProfileID == ProfileID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileReadingLanguageCode.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileReading>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileReadingDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileReadingDTO> result = new DTResult<ProfileReadingDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileReadingDTO> GetProfileReadingsByProfileID(int ProfileID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileReadingRepository().GetProfileReadingsByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileReading>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileReadingDTO> result = new PaginationSet<ProfileReadingDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileReadingDTO>>(queryList.ToList())
            };

            return result;
        }

        public ProfileReadingDTO Insert(ProfileReadingDTO objProfileReading)
        {
            ProfileReadingDTO result = Mapper.Map<ProfileReadingDTO>(DataLayer.DataLayerFacade.ProfileReadingRepository().Insert(objProfileReading.ProfileReadingLanguageCode, objProfileReading.ProfileReadingDatetime, objProfileReading.ProfileID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ProfileReadingDTO();
            }
        }

        public bool Delete(int ProfileReadingID)
        {
            bool result = DataLayer.DataLayerFacade.ProfileReadingRepository().Delete(ProfileReadingID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileReadingID, ProfileReadingDTO objProfileReading)
        {
            bool result = DataLayer.DataLayerFacade.ProfileReadingRepository().Update(objProfileReading.ProfileReadingID, objProfileReading.ProfileReadingLanguageCode, objProfileReading.ProfileReadingDatetime, objProfileReading.ProfileID);
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