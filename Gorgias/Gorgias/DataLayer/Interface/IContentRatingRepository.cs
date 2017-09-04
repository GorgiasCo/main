using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IContentRatingRepository
    {


        ContentRating Insert(String ContentRatingName, int ContentRatingAge, Boolean ContentRatingStatus, String ContentRatingImage, String ContentRatingDescription, int ContentRatingOrder, String ContentRatingLanguageCode, int? ContentRatingParentID);
        bool Update(int ContentRatingID, String ContentRatingName, int ContentRatingAge, Boolean ContentRatingStatus, String ContentRatingImage, String ContentRatingDescription, int ContentRatingOrder, String ContentRatingLanguageCode, int? ContentRatingParentID);
        bool Delete(int ContentRatingID);

        ContentRating GetContentRating(int ContentRatingID);

        //List
        List<ContentRating> GetContentRatingsAll();
        List<ContentRating> GetContentRatingsAll(bool ContentRatingStatus);
        List<ContentRating> GetContentRatingsAll(int page = 1, int pageSize = 7, string filter = null);
        List<ContentRating> GetContentRatingsAll(bool ContentRatingStatus, int page = 1, int pageSize = 7, string filter = null);

        List<ContentRating> GetContentRatingsByContentRatingParentID(int ContentRatingParentID, bool ContentRatingStatus);
        List<ContentRating> GetContentRatingsByContentRatingParentID(int ContentRatingParentID, int page = 1, int pageSize = 7, string filter = null);
        List<ContentRating> GetContentRatingsByContentRatingParentID(int ContentRatingParentID, bool ContentRatingStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<ContentRating> GetContentRatingsAllAsQueryable();
        IQueryable<ContentRating> GetContentRatingsAllAsQueryable(bool ContentRatingStatus);
        //V2
        IQueryable<Business.DataTransferObjects.Mobile.V2.ContentRatingMobileModel> GetContentRatingsAllAsQueryable(string languageCode);
        IQueryable<ContentRating> GetContentRatingsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ContentRating> GetContentRatingsAllAsQueryable(bool ContentRatingStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ContentRating> GetContentRatingsByContentRatingParentIDAsQueryable(int ContentRatingParentID);
        IQueryable<ContentRating> GetContentRatingsByContentRatingParentIDAsQueryable(int ContentRatingParentID, bool ContentRatingStatus);
        IQueryable<ContentRating> GetContentRatingsByContentRatingParentIDAsQueryable(int ContentRatingParentID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<ContentRating> GetContentRatingsByContentRatingParentIDAsQueryable(int ContentRatingParentID, bool ContentRatingStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


