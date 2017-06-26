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
    public class NewsletterFacade
    {                
        public NewsletterDTO GetNewsletter(int NewsletterID)
        {
            NewsletterDTO result = Mapper.Map<NewsletterDTO>(DataLayer.DataLayerFacade.NewsletterRepository().GetNewsletter(NewsletterID));             
            return result;
        }

        public NewsletterDTO GetNewsletter(string NewsletterName)
        {
            NewsletterDTO result = Mapper.Map<NewsletterDTO>(DataLayer.DataLayerFacade.NewsletterRepository().GetNewsletter(NewsletterName));
            return result;
        }

        public DTResult<NewsletterDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.NewsletterRepository().GetNewslettersAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.NewsletterName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Newsletter>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<NewsletterDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<NewsletterDTO> result = new DTResult<NewsletterDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<NewsletterDTO> GetNewsletters(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.NewsletterRepository().GetNewslettersAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Newsletter>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<NewsletterDTO> result = new PaginationSet<NewsletterDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<NewsletterDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<NewsletterDTO> GetNewsletters()
        {           
            var basequery = Mapper.Map <List<NewsletterDTO>>(DataLayer.DataLayerFacade.NewsletterRepository().GetNewslettersAllAsQueryable());
            return basequery.ToList();
        }

        

        public NewsletterDTO Insert(NewsletterDTO objNewsletter)
        {            
            NewsletterDTO result = Mapper.Map<NewsletterDTO>(DataLayer.DataLayerFacade.NewsletterRepository().Insert(objNewsletter.NewsletterName, objNewsletter.NewsletterNote, objNewsletter.NewsletterStatus));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new NewsletterDTO();
            }
        }
               
        public bool Delete(int NewsletterID)
        {            
            bool result = DataLayer.DataLayerFacade.NewsletterRepository().Delete(NewsletterID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int NewsletterID, NewsletterDTO objNewsletter)
        {            
            bool result = DataLayer.DataLayerFacade.NewsletterRepository().Update(objNewsletter.NewsletterID, objNewsletter.NewsletterName, objNewsletter.NewsletterNote, objNewsletter.NewsletterStatus);
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