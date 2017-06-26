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
    public class ProfileTagFacade
    {
        public ProfileTagDTO GetProfileTag(int TagID, int ProfileID)
        {
            ProfileTagDTO result = Mapper.Map<ProfileTagDTO>(DataLayer.DataLayerFacade.ProfileTagRepository().GetProfileTag(TagID, ProfileID));
            return result;
        }

        public DTResult<ProfileTagDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileTagRepository().GetProfileTagsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileTag>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileTagDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileTagDTO> result = new DTResult<ProfileTagDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ProfileTagDTO> GetProfileTags(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileTagRepository().GetProfileTagsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileTag>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileTagDTO> result = new PaginationSet<ProfileTagDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileTagDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ProfileTagDTO> GetProfileTags()
        {
            var basequery = Mapper.Map<List<ProfileTagDTO>>(DataLayer.DataLayerFacade.ProfileTagRepository().GetProfileTagsAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ProfileTagDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ProfileTagRepository().GetProfileTagsAllAsQueryable().Where(m => m.ProfileID == ProfileID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ProfileTag>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ProfileTagDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ProfileTagDTO> result = new DTResult<ProfileTagDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ProfileTagDTO> GetProfileTagsByProfileID(int ProfileID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ProfileTagRepository().GetProfileTagsByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ProfileTag>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ProfileTagDTO> result = new PaginationSet<ProfileTagDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ProfileTagDTO>>(queryList.ToList())
            };

            return result;
        }

        public ProfileTagDTO Insert(ProfileTagDTO objProfileTag)
        {
            if (!objProfileTag.TagName.Equals(""))
            {
                var resultTag = Facade.TagFacade().GetTag(objProfileTag.TagName);
                ProfileTagDTO result;

                if (resultTag != null)
                {
                    result = Mapper.Map<ProfileTagDTO>(DataLayer.DataLayerFacade.ProfileTagRepository().Insert(resultTag.TagID, objProfileTag.ProfileID, objProfileTag.ProfileTagStatus));
                }
                else
                {
                    var resultNewTag = Facade.TagFacade().Insert(new TagDTO { TagName = objProfileTag.TagName, TagStatus = true });
                    result = Mapper.Map<ProfileTagDTO>(DataLayer.DataLayerFacade.ProfileTagRepository().Insert(resultNewTag.TagID, objProfileTag.ProfileID, objProfileTag.ProfileTagStatus));
                }

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return new ProfileTagDTO();
                }
            }
            else {
                return new ProfileTagDTO();
            }            
        }

        public bool Delete(int TagID, int ProfileID)
        {
            bool result = DataLayer.DataLayerFacade.ProfileTagRepository().Delete(TagID, ProfileID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int TagID, int ProfileID, ProfileTagDTO objProfileTag)
        {
            bool result = DataLayer.DataLayerFacade.ProfileTagRepository().Update(objProfileTag.TagID, objProfileTag.ProfileID, objProfileTag.ProfileTagStatus);
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