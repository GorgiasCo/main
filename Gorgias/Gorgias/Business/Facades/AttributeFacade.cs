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
    public class AttributeFacade
    {                
        public AttributeDTO GetAttribute(int AttributeID)
        {
            AttributeDTO result = Mapper.Map<AttributeDTO>(DataLayer.DataLayerFacade.AttributeRepository().GetAttribute(AttributeID));             
            return result;
        }

        public DTResult<AttributeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.AttributeRepository().GetAttributesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.AttributeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Attribute>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AttributeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AttributeDTO> result = new DTResult<AttributeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<AttributeDTO> GetAttributes(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.AttributeRepository().GetAttributesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Attribute>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AttributeDTO> result = new PaginationSet<AttributeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AttributeDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<AttributeDTO> GetAttributes()
        {           
            var basequery = Mapper.Map <List<AttributeDTO>>(DataLayer.DataLayerFacade.AttributeRepository().GetAttributesAllAsQueryable());
            return basequery.ToList();
        }

        

        public AttributeDTO Insert(AttributeDTO objAttribute)
        {            
            AttributeDTO result = Mapper.Map<AttributeDTO>(DataLayer.DataLayerFacade.AttributeRepository().Insert(objAttribute.AttributeName, objAttribute.AttributeCaption, objAttribute.AttributeStatus, objAttribute.AttributeMode, objAttribute.AttributeRule, objAttribute.AttributeType, objAttribute.AttributeImage, objAttribute.AttributeDescription));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new AttributeDTO();
            }
        }
               
        public bool Delete(int AttributeID)
        {            
            bool result = DataLayer.DataLayerFacade.AttributeRepository().Delete(AttributeID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AttributeID, AttributeDTO objAttribute)
        {            
            bool result = DataLayer.DataLayerFacade.AttributeRepository().Update(objAttribute.AttributeID, objAttribute.AttributeName, objAttribute.AttributeCaption, objAttribute.AttributeStatus, objAttribute.AttributeMode, objAttribute.AttributeRule, objAttribute.AttributeType, objAttribute.AttributeImage, objAttribute.AttributeDescription);
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