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
    public class ProfileTokenFacade
    {
        //V2 Begin
        public bool RegisterProfileToken(Business.DataTransferObjects.Mobile.V2.ProfileTokenMobileModel profileToken)
        {
            if(DataLayer.DataLayerFacade.ProfileTokenRepository().Insert(profileToken.ProfileTokenString, DateTime.UtcNow,profileToken.ProfileID).ProfileID > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        //V2 End
        public ProfileTokenDTO GetProfileToken(int ProfileTokenID)
        {
            ProfileTokenDTO result = Mapper.Map<ProfileTokenDTO>(DataLayer.DataLayerFacade.ProfileTokenRepository().GetProfileToken(ProfileTokenID));
            return result;
        }

        public DTResult<ProfileTokenDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileTokenRepository().GetProfileTokensAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileTokenString.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileToken>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileTokenDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileTokenDTO> result = new DTResult<ProfileTokenDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ProfileTokenDTO> GetProfileTokens(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileTokenRepository().GetProfileTokensAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileToken>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileTokenDTO> result = new PaginationSet<ProfileTokenDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileTokenDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ProfileTokenDTO> GetProfileTokens()
        {
            var basequery = Mapper.Map<List<ProfileTokenDTO>>(DataLayer.DataLayerFacade.ProfileTokenRepository().GetProfileTokensAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ProfileTokenDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileTokenRepository().GetProfileTokensAllAsQueryable().Where(m => m.ProfileID == ProfileID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ProfileTokenString.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileToken>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileTokenDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileTokenDTO> result = new DTResult<ProfileTokenDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileTokenDTO> GetProfileTokensByProfileID(int ProfileID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileTokenRepository().GetProfileTokensByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileToken>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileTokenDTO> result = new PaginationSet<ProfileTokenDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileTokenDTO>>(queryList.ToList())
            };

            return result;
        }

        public ProfileTokenDTO Insert(ProfileTokenDTO objProfileToken)
        {
            ProfileTokenDTO result = Mapper.Map<ProfileTokenDTO>(DataLayer.DataLayerFacade.ProfileTokenRepository().Insert(objProfileToken.ProfileTokenString, objProfileToken.ProfileTokenRegistration, objProfileToken.ProfileID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ProfileTokenDTO();
            }
        }

        public bool Delete(int ProfileTokenID)
        {
            bool result = DataLayer.DataLayerFacade.ProfileTokenRepository().Delete(ProfileTokenID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileTokenID, ProfileTokenDTO objProfileToken)
        {
            bool result = DataLayer.DataLayerFacade.ProfileTokenRepository().Update(objProfileToken.ProfileTokenID, objProfileToken.ProfileTokenString, objProfileToken.ProfileTokenRegistration, objProfileToken.ProfileID);
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