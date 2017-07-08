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
        Album Insert(String AlbumName, DateTime AlbumDateCreated, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID);
        Album Insert(String AlbumName, DateTime AlbumDateCreated, DateTime AlbumDatePublish, int AlbumAvailability, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID, bool? AlbumHasComment);
        bool Update(int AlbumID, String AlbumName, DateTime AlbumDateCreated, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID);
        bool Update(int AlbumID, String AlbumName, DateTime AlbumDateCreated, DateTime AlbumDatePublish, int AlbumAvailability, Boolean AlbumStatus, String AlbumCover, Boolean AlbumIsDeleted, int CategoryID, int ProfileID, bool? AlbumHasComment);
        bool Update(int AlbumID);
        bool UpdateComment(int AlbumID);
        bool Delete(int AlbumID);

        Album GetAlbum(int AlbumID);

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


