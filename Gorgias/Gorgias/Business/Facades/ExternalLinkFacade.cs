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
    public class ExternalLinkFacade
    {                
        public ExternalLinkDTO GetExternalLink(int LinkTypeID, int ProfileID)
        {
            ExternalLinkDTO result = Mapper.Map<ExternalLinkDTO>(DataLayer.DataLayerFacade.ExternalLinkRepository().GetExternalLink(LinkTypeID, ProfileID));             
            return result;
        }

        public DTResult<ExternalLinkDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ExternalLinkRepository().GetExternalLinksAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ExternalLink>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ExternalLinkDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ExternalLinkDTO> result = new DTResult<ExternalLinkDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<ExternalLinkDTO> GetExternalLinks(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.ExternalLinkRepository().GetExternalLinksAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ExternalLink>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ExternalLinkDTO> result = new PaginationSet<ExternalLinkDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ExternalLinkDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<ExternalLinkDTO> GetExternalLinks()
        {           
            var basequery = Mapper.Map <List<ExternalLinkDTO>>(DataLayer.DataLayerFacade.ExternalLinkRepository().GetExternalLinksAllAsQueryable());
            return basequery.ToList();
        }

        
        public DTResult<ExternalLinkDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.ExternalLinkRepository().GetExternalLinksAllAsQueryable().Where(m=> m.ProfileID==ProfileID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<ExternalLink>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ExternalLinkDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ExternalLinkDTO> result = new DTResult<ExternalLinkDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<ExternalLinkDTO> GetExternalLinksByProfileID(int ProfileID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.ExternalLinkRepository().GetExternalLinksByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<ExternalLink>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ExternalLinkDTO> result = new PaginationSet<ExternalLinkDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ExternalLinkDTO>>(queryList.ToList())
            };

            return result;            
        }

        public ExternalLinkDTO Insert(ExternalLinkDTO objExternalLink)
        {            
            ExternalLinkDTO result = Mapper.Map<ExternalLinkDTO>(DataLayer.DataLayerFacade.ExternalLinkRepository().Insert(objExternalLink.LinkTypeID, objExternalLink.ProfileID, objExternalLink.ExternalLinkURL));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new ExternalLinkDTO();
            }
        }
               
        public bool Delete(int LinkTypeID, int ProfileID)
        {            
            bool result = DataLayer.DataLayerFacade.ExternalLinkRepository().Delete(LinkTypeID, ProfileID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int LinkTypeID, int ProfileID, ExternalLinkDTO objExternalLink)
        {            
            bool result = DataLayer.DataLayerFacade.ExternalLinkRepository().Update(objExternalLink.LinkTypeID, objExternalLink.ProfileID, objExternalLink.ExternalLinkURL);
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