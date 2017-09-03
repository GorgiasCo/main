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
    public class ContentTypeFacade
    {                
        public ContentTypeDTO GetContentType(int ContentTypeID)
        {
            ContentTypeDTO result = Mapper.Map<ContentTypeDTO>(DataLayer.DataLayerFacade.ContentTypeRepository().GetContentType(ContentTypeID));             
            return result;
        }

        public DTResult<ContentTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ContentTypeRepository().GetContentTypesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ContentTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ContentType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ContentTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ContentTypeDTO> result = new DTResult<ContentTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<ContentTypeDTO> GetContentTypes(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.ContentTypeRepository().GetContentTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ContentType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ContentTypeDTO> result = new PaginationSet<ContentTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ContentTypeDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<ContentTypeDTO> GetContentTypes()
        {           
            var basequery = Mapper.Map <List<ContentTypeDTO>>(DataLayer.DataLayerFacade.ContentTypeRepository().GetContentTypesAllAsQueryable());
            return basequery.ToList();
        }

        
        public DTResult<ContentTypeDTO> FilterResultByContentTypeParentID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ContentTypeParentID)
        {
            var basequery = DataLayer.DataLayerFacade.ContentTypeRepository().GetContentTypesAllAsQueryable().Where(m=> m.ContentTypeParentID==ContentTypeParentID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ContentTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ContentType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ContentTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ContentTypeDTO> result = new DTResult<ContentTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<ContentTypeDTO> GetContentTypesByContentTypeParentID(int ContentTypeParentID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.ContentTypeRepository().GetContentTypesByContentTypeParentIDAsQueryable(ContentTypeParentID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ContentType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ContentTypeDTO> result = new PaginationSet<ContentTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ContentTypeDTO>>(queryList.ToList())
            };

            return result;            
        }

        public ContentTypeDTO Insert(ContentTypeDTO objContentType)
        {            
            ContentTypeDTO result = Mapper.Map<ContentTypeDTO>(DataLayer.DataLayerFacade.ContentTypeRepository().Insert(objContentType.ContentTypeName, objContentType.ContentTypeOrder, objContentType.ContentTypeStatus, objContentType.ContentTypeLanguageCode, objContentType.ContentTypeExpression, objContentType.ContentTypeParentID));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new ContentTypeDTO();
            }
        }
               
        public bool Delete(int ContentTypeID)
        {            
            bool result = DataLayer.DataLayerFacade.ContentTypeRepository().Delete(ContentTypeID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ContentTypeID, ContentTypeDTO objContentType)
        {            
            bool result = DataLayer.DataLayerFacade.ContentTypeRepository().Update(objContentType.ContentTypeID, objContentType.ContentTypeName, objContentType.ContentTypeOrder, objContentType.ContentTypeStatus, objContentType.ContentTypeLanguageCode, objContentType.ContentTypeExpression, objContentType.ContentTypeParentID);
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