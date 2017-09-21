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
    public class IndustryFacade
    {
        //V2 Begin ;)
        public IQueryable<Business.DataTransferObjects.Mobile.V2.IndustryMobileModel> getIndustries(string languageCode)
        {
            return DataLayer.DataLayerFacade.IndustryRepository().GetIndustriesAsQueryable(languageCode);
        }

        public IQueryable<Business.DataTransferObjects.Mobile.V2.KeyValueMobileModel> getIndustriesByKeyValue(string languageCode)
        {
            return DataLayer.DataLayerFacade.IndustryRepository().GetIndustriesAsKeyValueQueryable(languageCode);
        }

        public IQueryable<IndustryDTO> getIndustriesByLanguageCode(string languageCode)
        {
            return DataLayer.DataLayerFacade.IndustryRepository().GetIndustriesAllAsQueryable(languageCode);
        }

        //V2 End ;)
        public IndustryDTO GetIndustry(int IndustryID)
        {
            IndustryDTO result = Mapper.Map<IndustryDTO>(DataLayer.DataLayerFacade.IndustryRepository().GetIndustry(IndustryID));
            return result;
        }

        public DTResult<IndustryDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.IndustryRepository().GetIndustriesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.IndustryName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Industry>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<IndustryDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<IndustryDTO> result = new DTResult<IndustryDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<IndustryDTO> GetIndustries(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.IndustryRepository().GetIndustriesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Industry>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<IndustryDTO> result = new PaginationSet<IndustryDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<IndustryDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<IndustryDTO> GetIndustries()
        {
            var basequery = Mapper.Map<List<IndustryDTO>>(DataLayer.DataLayerFacade.IndustryRepository().GetIndustriesAllAsQueryable().Where(m=> m.IndustryParentID == null));
            return basequery.ToList();
        }



        public IndustryDTO Insert(IndustryDTO objIndustry)
        {
            IndustryDTO result = Mapper.Map<IndustryDTO>(DataLayer.DataLayerFacade.IndustryRepository().Insert(objIndustry.IndustryName, objIndustry.IndustryStatus, objIndustry.IndustryParentID, objIndustry.IndustryImage, objIndustry.IndustryDescription));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new IndustryDTO();
            }
        }

        public IndustryDTO Insert(Industry objIndustry)
        {
            IndustryDTO result = Mapper.Map<IndustryDTO>(DataLayer.DataLayerFacade.IndustryRepository().Insert(objIndustry));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new IndustryDTO();
            }
        }

        public bool Delete(int IndustryID)
        {
            bool result = DataLayer.DataLayerFacade.IndustryRepository().Delete(IndustryID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int IndustryID, IndustryDTO objIndustry)
        {
            bool result = DataLayer.DataLayerFacade.IndustryRepository().Update(objIndustry.IndustryID, objIndustry.IndustryName, objIndustry.IndustryStatus, objIndustry.IndustryParentID, objIndustry.IndustryImage, objIndustry.IndustryDescription);
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