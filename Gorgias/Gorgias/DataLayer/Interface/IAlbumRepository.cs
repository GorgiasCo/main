using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IAlbumRepository
    {
        //V2 Begin ;)
        Album Insert(String AlbumName, Boolean AlbumStatus, String AlbumCover, int CategoryID, int ProfileID, DateTime AlbumDatePublish, int AlbumAvailability, Boolean? AlbumHasComment, String AlbumReadingLanguageCode, int? AlbumRepostValue, int? AlbumRepostRequest, int? AlbumRepostAttempt, Decimal? AlbumPrice, Boolean? AlbumIsTokenAvailable, int? AlbumPriceToken, int? ContentRatingID, ICollection<Business.DataTransferObjects.Mobile.V2.ContentUpdateMobileModel> Contents);
        Album Insert(String AlbumName, Boolean AlbumStatus, String AlbumCover, int CategoryID, int ProfileID, DateTime AlbumDatePublish, int AlbumAvailability, Boolean? AlbumHasComment, String AlbumReadingLanguageCode, int? AlbumRepostValue, int? AlbumRepostRequest, int? AlbumRepostAttempt, Decimal? AlbumPrice, Boolean? AlbumIsTokenAvailable, int? AlbumPriceToken, int? ContentRatingID, ICollection<Business.DataTransferObjects.Mobile.V2.ContentUpdateMobileModel> Contents, int? AlbumParentID);
        Album Insert(String AlbumName, Boolean AlbumStatus, String AlbumCover, int CategoryID, int ProfileID, DateTime AlbumDatePublish, int AlbumAvailability, Boolean? AlbumHasComment, String AlbumReadingLanguageCode, int? AlbumRepostValue, int? AlbumRepostRequest, int? AlbumRepostAttempt, Decimal? AlbumPrice, Boolean? AlbumIsTokenAvailable, int? AlbumPriceToken, int? ContentRatingID, ICollection<Business.DataTransferObjects.Mobile.V2.ContentUpdateMobileModel> Contents, Business.DataTransferObjects.Mobile.V2.CategoryNewMobileModel Topic, int? AlbumParentID);
        bool Update(int AlbumID, String AlbumName, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID, int AlbumView, DateTime AlbumDatePublish, int AlbumAvailability, Boolean? AlbumHasComment, String AlbumReadingLanguageCode, int? AlbumRepostValue, int? AlbumRepostRequest, int? AlbumRepostAttempt, Decimal? AlbumPrice, Boolean? AlbumIsTokenAvailable, int? AlbumPriceToken, int? ContentRatingID);
        bool UpdatePublishAlbum(int AlbumID);
        bool UpdateRepostAlbum(int AlbumID);
        bool UpdateRequestRepostAlbum(int AlbumID);
        //V2 End ;)
        Album Insert(String AlbumName, DateTime AlbumDateCreated, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID);
        Album Insert(String AlbumName, DateTime AlbumDateCreated, DateTime AlbumDatePublish, int AlbumAvailability, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID, bool? AlbumHasComment);
        bool Update(int AlbumID, String AlbumName, DateTime AlbumDateCreated, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID);
        bool Update(int AlbumID, String AlbumName, DateTime AlbumDateCreated, DateTime AlbumDatePublish, int AlbumAvailability, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID, bool? AlbumHasComment);
        bool Update(int AlbumID);
        bool UpdateComment(int AlbumID);
        bool Delete(int AlbumID);

        Album GetAlbum(int AlbumID);
        Business.DataTransferObjects.Mobile.V2.AlbumUpdateMobileModel GetAlbumV2(int AlbumID);
        Album GetAlbumV2Mobile(int AlbumID, int ProfileID);
        int? GetAlbumViewsV2Mobile(int ProfileID);

        //List
        List<Album> GetAlbumsAll();
        List<Album> GetAlbumsAll(bool AlbumStatus);
        List<Album> GetAlbumsAll(int page = 1, int pageSize = 7, string filter = null);
        List<Album> GetAlbumsAll(bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null);

        List<Album> GetAlbumsByCategoryID(int CategoryID, bool AlbumStatus);
        List<Album> GetAlbumsByCategoryID(int CategoryID, int page = 1, int pageSize = 7, string filter = null);
        List<Album> GetAlbumsByCategoryID(int CategoryID, bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null);
        List<Album> GetAlbumsByProfileID(int ProfileID, bool AlbumStatus);
        List<Album> GetAlbumsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
        List<Album> GetAlbumsByProfileID(int ProfileID, bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null);

        object getAlbumSet(int ProfileID);

        Business.DataTransferObjects.Mobile.AlbumMobileModel GetAlbumContent(int AlbumID);
        IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetAlbumContentsAsQueryable(int ProfileID, int page = 1, int pageSize = 7, int contentSize = 6);
        IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetV2AlbumContentsAsQueryable(int page = 1, int pageSize = 7, int contentSize = 6);
        IQueryable<Business.DataTransferObjects.Mobile.V2.AlbumMobileModel> GetV2AlbumContentsByCategoryAsQueryable(int CategoryID);

        IQueryable<Album> GetV2AlbumByCategoryAsQueryable();
        IQueryable<Album> GetV3AlbumByCategoryAsQueryable();
        IQueryable<Album> GetV2AlbumByCategoryAsQueryable(int CategoryID);
        IQueryable<Album> GetV2AlbumByCategoryForMicroAppAsQueryable(int CategoryID);
        IQueryable<Album> GetV2AlbumByCategoryAsQueryable(int CategoryID, int ProfileID);

        IList<Business.DataTransferObjects.Mobile.AlbumMobileAdminModel> GetAdminAlbumContentsAsQueryable(int ProfileID, int page = 1, int pageSize = 7, int contentSize = 6);
        System.Threading.Tasks.Task<IList<Business.DataTransferObjects.Mobile.AlbumMobileAdminModel>> GetAdminAlbumContentsAsQueryableAsync(int ProfileID, int page = 1, int pageSize = 7, int contentSize = 6);
        IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetAlbumHottestContentsAsQueryable(int ProfileID, int CategoryID, int page = 1, int pageSize = 7);
        IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetAlbumGalleryContentsAsQueryable(int ProfileID, int page = 1, int pageSize = 7);
        IList<Business.DataTransferObjects.Mobile.AlbumMobileModel> GetAlbumGalleryContentsAsQueryable(int ProfileID, int CategoryID, int page = 1, int pageSize = 7);


        //IQueryable
        IQueryable<Business.DataTransferObjects.AlbumContentDTO> GetAlbumsAllForInsertContentAsQueryable();
        IQueryable<Album> GetAlbumsAllAsQueryable();
        IQueryable<Album> GetAlbumsAllAsQueryable(bool AlbumStatus);
        IQueryable<Album> GetAlbumsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Album> GetAlbumsAllAsQueryable(bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Album> GetAlbumsByCategoryIDAsQueryable(int CategoryID);
        IQueryable<Album> GetAlbumsByCategoryIDAsQueryable(int CategoryID, bool AlbumStatus);
        IQueryable<Album> GetAlbumsByCategoryIDAsQueryable(int CategoryID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Album> GetAlbumsByCategoryIDAsQueryable(int CategoryID, bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Album> GetAlbumsByProfileIDAsQueryable(int ProfileID);
        IQueryable<Album> GetAlbumsByProfileIDAsQueryable(int ProfileID, bool AlbumStatus);
        IQueryable<Album> GetAlbumsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Album> GetAlbumsByProfileIDAsQueryable(int ProfileID, bool AlbumStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


