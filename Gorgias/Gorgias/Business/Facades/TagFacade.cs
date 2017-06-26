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
    public class TagFacade
    {
        public TagDTO GetTag(int TagID)
        {
            TagDTO result = Mapper.Map<TagDTO>(DataLayer.DataLayerFacade.TagRepository().GetTag(TagID));
            return result;
        }

        public TagDTO GetTag(string TagName)
        {
            TagDTO result = Mapper.Map<TagDTO>(DataLayer.DataLayerFacade.TagRepository().GetTag(TagName));
            return result;
        }

        public DTResult<TagDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.TagRepository().GetTagsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.TagName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Tag>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<TagDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<TagDTO> result = new DTResult<TagDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<TagDTO> GetTags(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.TagRepository().GetTagsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Tag>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<TagDTO> result = new PaginationSet<TagDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<TagDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<TagDTO> GetTags()
        {
            var basequery = Mapper.Map<List<TagDTO>>(DataLayer.DataLayerFacade.TagRepository().GetTagsAllAsQueryable());
            return basequery.ToList();
        }

        public TagDTO Insert(TagDTO objTag)
        {
            TagDTO result = Mapper.Map<TagDTO>(DataLayer.DataLayerFacade.TagRepository().Insert(objTag.TagName, objTag.TagStatus, objTag.TagIsPrimary, objTag.TagWeight));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new TagDTO();
            }
        }

        public bool Delete(int TagID)
        {
            bool result = DataLayer.DataLayerFacade.TagRepository().Delete(TagID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int TagID, TagDTO objTag)
        {
            bool result = DataLayer.DataLayerFacade.TagRepository().Update(objTag.TagID, objTag.TagName, objTag.TagStatus, objTag.TagIsPrimary, objTag.TagWeight);
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