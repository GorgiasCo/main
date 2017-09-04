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
    public class LanguageFacade
    {                
        public LanguageDTO GetLanguage(int LanguageID)
        {
            LanguageDTO result = Mapper.Map<LanguageDTO>(DataLayer.DataLayerFacade.LanguageRepository().GetLanguage(LanguageID));             
            return result;
        }

        public DTResult<LanguageDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.LanguageRepository().GetLanguagesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.LanguageName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Language>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<LanguageDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<LanguageDTO> result = new DTResult<LanguageDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<LanguageDTO> GetLanguages(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.LanguageRepository().GetLanguagesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Language>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<LanguageDTO> result = new PaginationSet<LanguageDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<LanguageDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<LanguageDTO> GetLanguages()
        {           
            var basequery = Mapper.Map <List<LanguageDTO>>(DataLayer.DataLayerFacade.LanguageRepository().GetLanguagesAllAsQueryable());
            return basequery.ToList();
        }

        

        public LanguageDTO Insert(LanguageDTO objLanguage)
        {            
            LanguageDTO result = Mapper.Map<LanguageDTO>(DataLayer.DataLayerFacade.LanguageRepository().Insert(objLanguage.LanguageName, objLanguage.LanguageCode, objLanguage.LanguageStatus));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new LanguageDTO();
            }
        }
               
        public bool Delete(int LanguageID)
        {            
            bool result = DataLayer.DataLayerFacade.LanguageRepository().Delete(LanguageID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int LanguageID, LanguageDTO objLanguage)
        {            
            bool result = DataLayer.DataLayerFacade.LanguageRepository().Update(objLanguage.LanguageID, objLanguage.LanguageName, objLanguage.LanguageCode, objLanguage.LanguageStatus);
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