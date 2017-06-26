using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface ICommentRepository
    {
    
    
        Comment Insert(string CommentNote, int CommentLike, DateTime CommentDateTime, bool CommentStatus, int ProfileID, int ContentID);
        bool Update(int CommentID, string CommentNote, int CommentLike, DateTime CommentDateTime, bool CommentStatus, int ProfileID, int ContentID);        
        bool Delete(int CommentID);

        Comment GetComment(int CommentID);

        //List
        List<Comment> GetCommentsAll();
        List<Comment> GetCommentsAll(bool CommentStatus);
        List<Comment> GetCommentsAll(int page = 1, int pageSize = 7, string filter=null);
        List<Comment> GetCommentsAll(bool CommentStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        List<Comment> GetCommentsByProfileID(int ProfileID, bool CommentStatus);
        List<Comment> GetCommentsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        List<Comment> GetCommentsByProfileID(int ProfileID, bool CommentStatus, int page = 1, int pageSize = 7, string filter=null);               
        List<Comment> GetCommentsByContentID(int ContentID, bool CommentStatus);
        List<Comment> GetCommentsByContentID(int ContentID, int page = 1, int pageSize = 7, string filter=null);       
        List<Comment> GetCommentsByContentID(int ContentID, bool CommentStatus, int page = 1, int pageSize = 7, string filter=null);               
        
        //IQueryable
        IQueryable<Comment> GetCommentsAllAsQueryable();
        IQueryable<Comment> GetCommentsAllAsQueryable(bool CommentStatus);
        IQueryable<Comment> GetCommentsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Comment> GetCommentsAllAsQueryable(bool CommentStatus, int page = 1, int pageSize = 7, string filter=null);   
        IQueryable<Comment> GetCommentsByProfileIDAsQueryable(int ProfileID);
        IQueryable<Comment> GetCommentsByProfileIDAsQueryable(int ProfileID, bool CommentStatus);
        IQueryable<Comment> GetCommentsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<Comment> GetCommentsByProfileIDAsQueryable(int ProfileID, bool CommentStatus, int page = 1, int pageSize = 7, string filter=null);               
        IQueryable<Comment> GetCommentsByContentIDAsQueryable(int ContentID);
        IQueryable<Business.DataTransferObjects.Mobile.CommentModel> GetCommentsByContentIDForMobileAsQueryable(int ContentID);
        IQueryable<Comment> GetCommentsByContentIDAsQueryable(int ContentID, bool CommentStatus);
        IQueryable<Comment> GetCommentsByContentIDAsQueryable(int ContentID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<Comment> GetCommentsByContentIDAsQueryable(int ContentID, bool CommentStatus, int page = 1, int pageSize = 7, string filter=null);               
    }
}


