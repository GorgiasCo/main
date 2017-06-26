using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface ITagRepository
    {
        Tag Insert(String TagName, Boolean TagStatus, bool TagIsPrimary, int TagWeight);
        bool Update(int TagID, String TagName, Boolean TagStatus, bool TagIsPrimary, int TagWeight);
        bool Delete(int TagID);

        Tag GetTag(int TagID);
        Tag GetTag(string TagName);

        //List
        List<Tag> GetTagsAll();
        List<Tag> GetTagsAll(bool TagStatus);
        List<Tag> GetTagsAll(int page = 1, int pageSize = 7, string filter = null);
        List<Tag> GetTagsAll(bool TagStatus, int page = 1, int pageSize = 7, string filter = null);


        //IQueryable
        IQueryable<Tag> GetTagsAllAsQueryable();
        IQueryable<Tag> GetTagsAllAsQueryable(bool TagStatus);
        IQueryable<Tag> GetTagsAllAsQueryable(bool TagStatus, bool TagIsPrimary);
        IQueryable<Tag> GetTagsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Tag> GetTagsAllAsQueryable(bool TagStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


