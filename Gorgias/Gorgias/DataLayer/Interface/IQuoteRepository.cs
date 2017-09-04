using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IQuoteRepository
    {
        Quote Insert(String QuoteName, Boolean QuoteStatus, String QuoteLanguageCode, int QuoteProbability, int CategoryID);
        bool Update(int QuoteID, String QuoteName, Boolean QuoteStatus, String QuoteLanguageCode, int QuoteProbability, int CategoryID);
        bool Delete(int QuoteID);

        Quote GetQuote(int QuoteID);

        //List
        List<Quote> GetQuotesAll();
        List<Quote> GetQuotesAll(bool QuoteStatus);
        List<Quote> GetQuotesAll(int page = 1, int pageSize = 7, string filter = null);
        List<Quote> GetQuotesAll(bool QuoteStatus, int page = 1, int pageSize = 7, string filter = null);

        List<Quote> GetQuotesByCategoryID(int CategoryID, bool QuoteStatus);
        List<Quote> GetQuotesByCategoryID(int CategoryID, int page = 1, int pageSize = 7, string filter = null);
        List<Quote> GetQuotesByCategoryID(int CategoryID, bool QuoteStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<Business.DataTransferObjects.Mobile.V2.QuoteMobileModel> GetQuotesAsQueryable(string languageCode);
        IQueryable<Quote> GetQuotesAllAsQueryable();
        IQueryable<Quote> GetQuotesAllAsQueryable(bool QuoteStatus);
        IQueryable<Quote> GetQuotesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Quote> GetQuotesAllAsQueryable(bool QuoteStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Quote> GetQuotesByCategoryIDAsQueryable(int CategoryID);
        IQueryable<Quote> GetQuotesByCategoryIDAsQueryable(int CategoryID, bool QuoteStatus);
        IQueryable<Quote> GetQuotesByCategoryIDAsQueryable(int CategoryID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Quote> GetQuotesByCategoryIDAsQueryable(int CategoryID, bool QuoteStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


