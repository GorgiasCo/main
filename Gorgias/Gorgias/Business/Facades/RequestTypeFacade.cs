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
    public class RequestTypeFacade
    {                
        public RequestTypeDTO GetRequestType(int RequestTypeID)
        {
            RequestTypeDTO result = Mapper.Map<RequestTypeDTO>(DataLayer.DataLayerFacade.RequestTypeRepository().GetRequestType(RequestTypeID));             
            return result;
        }

        public DTResult<RequestTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.RequestTypeRepository().GetRequestTypesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.RequestTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<RequestType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<RequestTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<RequestTypeDTO> result = new DTResult<RequestTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<RequestTypeDTO> GetRequestTypes(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.RequestTypeRepository().GetRequestTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<RequestType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<RequestTypeDTO> result = new PaginationSet<RequestTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<RequestTypeDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<RequestTypeDTO> GetRequestTypes()
        {           
            var basequery = Mapper.Map <List<RequestTypeDTO>>(DataLayer.DataLayerFacade.RequestTypeRepository().GetRequestTypesAllAsQueryable());
            return basequery.ToList();
        }

        

        public RequestTypeDTO Insert(RequestTypeDTO objRequestType)
        {            
            RequestTypeDTO result = Mapper.Map<RequestTypeDTO>(DataLayer.DataLayerFacade.RequestTypeRepository().Insert(objRequestType.RequestTypeName, objRequestType.RequestTypeStatus, objRequestType.RequestIsRestricted));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new RequestTypeDTO();
            }
        }
               
        public bool Delete(int RequestTypeID)
        {            
            bool result = DataLayer.DataLayerFacade.RequestTypeRepository().Delete(RequestTypeID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int RequestTypeID, RequestTypeDTO objRequestType)
        {            
            bool result = DataLayer.DataLayerFacade.RequestTypeRepository().Update(objRequestType.RequestTypeID, objRequestType.RequestTypeName, objRequestType.RequestTypeStatus, objRequestType.RequestIsRestricted);
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