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
    public class ContentRatingFacade
    {
        public IQueryable<Business.DataTransferObjects.Mobile.V2.ContentRatingMobileModel> getContentRatings(string languageCode)
        {
            return DataLayer.DataLayerFacade.ContentRatingRepository().GetContentRatingsAllAsQueryable(languageCode);
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> getContentRatingsByKeyValue(string languageCode)
        {
            return DataLayer.DataLayerFacade.ContentRatingRepository().GetContentRatingsAllAsQueryableByKeyValue(languageCode);
        }

        public ContentRatingDTO GetContentRating(int ContentRatingID)
        {
            ContentRatingDTO result = Mapper.Map<ContentRatingDTO>(DataLayer.DataLayerFacade.ContentRatingRepository().GetContentRating(ContentRatingID));
            return result;
        }

        public DTResult<ContentRatingDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ContentRatingRepository().GetContentRatingsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ContentRatingName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ContentRating>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ContentRatingDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ContentRatingDTO> result = new DTResult<ContentRatingDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ContentRatingDTO> GetContentRatings(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ContentRatingRepository().GetContentRatingsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ContentRating>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ContentRatingDTO> result = new PaginationSet<ContentRatingDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ContentRatingDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ContentRatingDTO> GetContentRatings()
        {
            var basequery = Mapper.Map<List<ContentRatingDTO>>(DataLayer.DataLayerFacade.ContentRatingRepository().GetContentRatingsAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ContentRatingDTO> FilterResultByContentRatingParentID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ContentRatingParentID)
        {
            var basequery = DataLayer.DataLayerFacade.ContentRatingRepository().GetContentRatingsAllAsQueryable().Where(m => m.ContentRatingParentID == ContentRatingParentID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ContentRatingName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ContentRating>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ContentRatingDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ContentRatingDTO> result = new DTResult<ContentRatingDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ContentRatingDTO> GetContentRatingsByContentRatingParentID(int ContentRatingParentID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ContentRatingRepository().GetContentRatingsByContentRatingParentIDAsQueryable(ContentRatingParentID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ContentRating>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ContentRatingDTO> result = new PaginationSet<ContentRatingDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ContentRatingDTO>>(queryList.ToList())
            };

            return result;
        }

        public ContentRatingDTO Insert(ContentRatingDTO objContentRating)
        {
            ContentRatingDTO result = Mapper.Map<ContentRatingDTO>(DataLayer.DataLayerFacade.ContentRatingRepository().Insert(objContentRating.ContentRatingName, objContentRating.ContentRatingAge, objContentRating.ContentRatingStatus, objContentRating.ContentRatingImage, objContentRating.ContentRatingDescription, objContentRating.ContentRatingOrder, objContentRating.ContentRatingLanguageCode, objContentRating.ContentRatingParentID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ContentRatingDTO();
            }
        }

        public bool Delete(int ContentRatingID)
        {
            bool result = DataLayer.DataLayerFacade.ContentRatingRepository().Delete(ContentRatingID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ContentRatingID, ContentRatingDTO objContentRating)
        {
            bool result = DataLayer.DataLayerFacade.ContentRatingRepository().Update(objContentRating.ContentRatingID, objContentRating.ContentRatingName, objContentRating.ContentRatingAge, objContentRating.ContentRatingStatus, objContentRating.ContentRatingImage, objContentRating.ContentRatingDescription, objContentRating.ContentRatingOrder, objContentRating.ContentRatingLanguageCode, objContentRating.ContentRatingParentID);
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