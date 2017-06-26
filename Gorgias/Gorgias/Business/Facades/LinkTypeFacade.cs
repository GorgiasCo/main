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
    public class LinkTypeFacade
    {                
        public LinkTypeDTO GetLinkType(int LinkTypeID)
        {
            LinkTypeDTO result = Mapper.Map<LinkTypeDTO>(DataLayer.DataLayerFacade.LinkTypeRepository().GetLinkType(LinkTypeID));             
            return result;
        }

        public DTResult<LinkTypeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.LinkTypeRepository().GetLinkTypesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.LinkTypeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<LinkType>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<LinkTypeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<LinkTypeDTO> result = new DTResult<LinkTypeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<LinkTypeDTO> GetLinkTypes(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.LinkTypeRepository().GetLinkTypesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<LinkType>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<LinkTypeDTO> result = new PaginationSet<LinkTypeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<LinkTypeDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<LinkTypeDTO> GetLinkTypes()
        {           
            var basequery = Mapper.Map <List<LinkTypeDTO>>(DataLayer.DataLayerFacade.LinkTypeRepository().GetLinkTypesAllAsQueryable());
            return basequery.ToList();
        }

        

        public LinkTypeDTO Insert(LinkTypeDTO objLinkType)
        {            
            LinkTypeDTO result = Mapper.Map<LinkTypeDTO>(DataLayer.DataLayerFacade.LinkTypeRepository().Insert(objLinkType.LinkTypeName, objLinkType.LinkTypeStatus, objLinkType.LinkTypeImage, objLinkType.LinkTypeDescription));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new LinkTypeDTO();
            }
        }
               
        public bool Delete(int LinkTypeID)
        {            
            bool result = DataLayer.DataLayerFacade.LinkTypeRepository().Delete(LinkTypeID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int LinkTypeID, LinkTypeDTO objLinkType)
        {            
            bool result = DataLayer.DataLayerFacade.LinkTypeRepository().Update(objLinkType.LinkTypeID, objLinkType.LinkTypeName, objLinkType.LinkTypeStatus, objLinkType.LinkTypeImage, objLinkType.LinkTypeDescription);
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