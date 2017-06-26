using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IContentRepository
    {
        Content Insert(String ContentTitle, String ContentURL, int ContentType, Boolean ContentStatus, Boolean ContentIsDeleted, DateTime? ContentCreatedDate, int AlbumID);        
        bool Update(int ContentID, String ContentTitle, String ContentURL, int ContentType, Boolean ContentStatus, Boolean ContentIsDeleted, int AlbumID);
        bool Update(int ContentID, int ContentLike);
        bool Delete(int ContentID);

        Content GetContent(int ContentID);

        //List
        List<Content> GetContentsAll();
        List<Content> GetContentsAll(bool ContentStatus);
        List<Content> GetContentsAll(int page = 1, int pageSize = 7, string filter = null);
        List<Content> GetContentsAll(bool ContentStatus, int page = 1, int pageSize = 7, string filter = null);

        List<Content> GetContentsByAlbumID(int AlbumID, bool ContentStatus);
        List<Content> GetContentsByAlbumID(int AlbumID, int page = 1, int pageSize = 7, string filter = null);
        List<Content> GetContentsByAlbumID(int AlbumID, bool ContentStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<Content> GetContentsAllAsQueryable();
        IQueryable<Content> GetContentsAllAsQueryable(bool ContentStatus);
        IQueryable<Content> GetContentsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Content> GetContentsAllAsQueryable(bool ContentStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Content> GetContentsByAlbumIDAsQueryable(int AlbumID);
        IQueryable<Content> GetContentsByAlbumIDAsQueryable(int AlbumID, bool ContentStatus);
        IQueryable<Content> GetContentsByAlbumIDAsQueryable(int AlbumID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Content> GetContentsByAlbumIDAsQueryable(int AlbumID, bool ContentStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


