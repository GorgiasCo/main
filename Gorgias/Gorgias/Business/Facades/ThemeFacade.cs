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
    public class ThemeFacade
    {                
        public ThemeDTO GetTheme(int ThemeID)
        {
            ThemeDTO result = Mapper.Map<ThemeDTO>(DataLayer.DataLayerFacade.ThemeRepository().GetTheme(ThemeID));             
            return result;
        }

        public DTResult<ThemeDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ThemeRepository().GetThemesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.ThemeName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Theme>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ThemeDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ThemeDTO> result = new DTResult<ThemeDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<ThemeDTO> GetThemes(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.ThemeRepository().GetThemesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Theme>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ThemeDTO> result = new PaginationSet<ThemeDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ThemeDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<ThemeDTO> GetThemes()
        {           
            var basequery = Mapper.Map <List<ThemeDTO>>(DataLayer.DataLayerFacade.ThemeRepository().GetThemesAllAsQueryable());
            return basequery.ToList();
        }

        

        public ThemeDTO Insert(ThemeDTO objTheme)
        {            
            ThemeDTO result = Mapper.Map<ThemeDTO>(DataLayer.DataLayerFacade.ThemeRepository().Insert(objTheme.ThemeName, objTheme.ThemeClassCode, objTheme.ThemeStatus));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new ThemeDTO();
            }
        }
               
        public bool Delete(int ThemeID)
        {            
            bool result = DataLayer.DataLayerFacade.ThemeRepository().Delete(ThemeID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ThemeID, ThemeDTO objTheme)
        {            
            bool result = DataLayer.DataLayerFacade.ThemeRepository().Update(objTheme.ThemeID, objTheme.ThemeName, objTheme.ThemeClassCode, objTheme.ThemeStatus);
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