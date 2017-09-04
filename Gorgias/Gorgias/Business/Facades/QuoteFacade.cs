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
    public class QuoteFacade
    {
        public IQueryable<Business.DataTransferObjects.Mobile.V2.QuoteMobileModel> getQuotes(string languageCode)
        {
            return DataLayer.DataLayerFacade.QuoteRepository().GetQuotesAsQueryable(languageCode);
        }

        public QuoteDTO GetQuote(int QuoteID)
        {
            QuoteDTO result = Mapper.Map<QuoteDTO>(DataLayer.DataLayerFacade.QuoteRepository().GetQuote(QuoteID));
            return result;
        }

        public DTResult<QuoteDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.QuoteRepository().GetQuotesAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.QuoteName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Quote>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<QuoteDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<QuoteDTO> result = new DTResult<QuoteDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<QuoteDTO> GetQuotes(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.QuoteRepository().GetQuotesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Quote>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<QuoteDTO> result = new PaginationSet<QuoteDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<QuoteDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<QuoteDTO> GetQuotes()
        {
            var basequery = Mapper.Map<List<QuoteDTO>>(DataLayer.DataLayerFacade.QuoteRepository().GetQuotesAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<QuoteDTO> FilterResultByCategoryID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int CategoryID)
        {
            var basequery = DataLayer.DataLayerFacade.QuoteRepository().GetQuotesAllAsQueryable().Where(m => m.CategoryID == CategoryID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.QuoteName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Quote>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<QuoteDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<QuoteDTO> result = new DTResult<QuoteDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<QuoteDTO> GetQuotesByCategoryID(int CategoryID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.QuoteRepository().GetQuotesByCategoryIDAsQueryable(CategoryID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Quote>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<QuoteDTO> result = new PaginationSet<QuoteDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<QuoteDTO>>(queryList.ToList())
            };

            return result;
        }

        public QuoteDTO Insert(QuoteDTO objQuote)
        {
            QuoteDTO result = Mapper.Map<QuoteDTO>(DataLayer.DataLayerFacade.QuoteRepository().Insert(objQuote.QuoteName, objQuote.QuoteStatus, objQuote.QuoteLanguageCode, objQuote.QuoteProbability, objQuote.CategoryID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new QuoteDTO();
            }
        }

        public bool Delete(int QuoteID)
        {
            bool result = DataLayer.DataLayerFacade.QuoteRepository().Delete(QuoteID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int QuoteID, QuoteDTO objQuote)
        {
            bool result = DataLayer.DataLayerFacade.QuoteRepository().Update(objQuote.QuoteID, objQuote.QuoteName, objQuote.QuoteStatus, objQuote.QuoteLanguageCode, objQuote.QuoteProbability, objQuote.CategoryID);
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