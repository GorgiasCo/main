using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Gorgias;
using Gorgias.DataLayer.Interface;
using Gorgias.Infrastruture.EntityFramework;
using System.Linq;
namespace Gorgias.DataLayer.Repository.SQL
{   
	public class CommentRepository : ICommentRepository, IDisposable
	{
     	// To detect redundant calls
		private bool disposedValue = false;

		private GorgiasEntities context = new GorgiasEntities();

		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue) {
				if (!this.disposedValue) {
					if (disposing) {
						context.Dispose();
					}
				}
				this.disposedValue = true;
			}
			this.disposedValue = true;
		}

		#region " IDisposable Support "
		// This code added by Visual Basic to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
        
        //CRUD Functions
		public Comment Insert(String CommentNote, int CommentLike, DateTime CommentDateTime, Boolean CommentStatus, int ProfileID, int ContentID)
		{
            try{
                Comment obj = new Comment();	
                
                
            		obj.CommentNote = CommentNote;			
            		obj.CommentLike = CommentLike;			
            		obj.CommentDateTime = CommentDateTime;			
            		obj.CommentStatus = CommentStatus;			
            		obj.ProfileID = ProfileID;			
            		obj.ContentID = ContentID;			
    			context.Comments.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new Comment();
            }
		}

		public bool Update(int CommentID, String CommentNote, int CommentLike, DateTime CommentDateTime, Boolean CommentStatus, int ProfileID, int ContentID)
		{
		    Comment obj = new Comment();
            obj = (from w in context.Comments where w.CommentID == CommentID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Comments.Attach(obj);
	
        		obj.CommentNote = CommentNote;			
        		obj.CommentLike = CommentLike;			
        		obj.CommentDateTime = CommentDateTime;			
        		obj.CommentStatus = CommentStatus;			
        		obj.ProfileID = ProfileID;			
        		obj.ContentID = ContentID;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int CommentID)
		{
			Comment obj = new Comment();
			obj = (from w in context.Comments where  w.CommentID == CommentID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Comments.Attach(obj);
			    context.Comments.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public Comment GetComment(int CommentID)
		{
			return (from w in context.Comments where  w.CommentID == CommentID  select w).FirstOrDefault();
		}

        //Lists
		public List<Comment> GetCommentsAll()
		{
			return (from w in context.Comments orderby w.CommentID descending select w).ToList();
		}
		public List<Comment> GetCommentsAll(bool CommentStatus)
		{
			return (from w in context.Comments where w.CommentStatus == CommentStatus orderby w.CommentID descending select w).ToList();
		}
        //List Pagings
        public List<Comment> GetCommentsAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<Comment>();
            if (filter != null)
            {
                xList = (from w in context.Comments orderby w.CommentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Comments orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<Comment> GetCommentsAll(bool CommentStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<Comment>();
            if (filter != null)
            {
                xList = (from w in context.Comments where w.CommentStatus == CommentStatus  orderby w.CommentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Comments where w.CommentStatus == CommentStatus orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<Comment> GetCommentsAllAsQueryable()
		{
			return (from w in context.Comments orderby w.CommentID descending select w).AsQueryable();
		}
		public IQueryable<Comment> GetCommentsAllAsQueryable(bool CommentStatus)
		{
			return (from w in context.Comments where w.CommentStatus == CommentStatus orderby w.CommentID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<Comment> GetCommentsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<Comment> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Comments orderby w.CommentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Comments.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Comments orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Comments.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<Comment> GetCommentsAllAsQueryable(bool CommentStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<Comment> xList;
            if (filter != null)
            {
                xList = (from w in context.Comments where w.CommentStatus == CommentStatus orderby w.CommentID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.Comments where w.CommentStatus == CommentStatus orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Comment> GetCommentsByProfileID(int ProfileID, bool CommentStatus)
		{
			return (from w in context.Comments where w.ProfileID == ProfileID && w.CommentStatus == CommentStatus orderby w.CommentID descending select w).ToList();
		}
        public List<Comment> GetCommentsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Comments where w.ProfileID == ProfileID orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Comment> GetCommentsByProfileID(int ProfileID, bool CommentStatus, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Comments where w.ProfileID == ProfileID && w.CommentStatus == CommentStatus orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Comment> GetCommentsByContentID(int ContentID, bool CommentStatus)
		{
			return (from w in context.Comments where w.ContentID == ContentID && w.CommentStatus == CommentStatus orderby w.CommentID descending select w).ToList();
		}
        public List<Comment> GetCommentsByContentID(int ContentID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Comments where w.ContentID == ContentID orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Comment> GetCommentsByContentID(int ContentID, bool CommentStatus, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Comments where w.ContentID == ContentID && w.CommentStatus == CommentStatus orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Comment> GetCommentsByProfileIDAsQueryable(int ProfileID)
		{
			return (from w in context.Comments where w.ProfileID == ProfileID orderby w.CommentID descending select w).AsQueryable();
		}
        public IQueryable<Comment> GetCommentsByProfileIDAsQueryable(int ProfileID, bool CommentStatus)
		{
			return (from w in context.Comments where w.ProfileID == ProfileID && w.CommentStatus == CommentStatus orderby w.CommentID descending select w).AsQueryable();
		}
        public IQueryable<Comment> GetCommentsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Comments where w.ProfileID == ProfileID orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Comment> GetCommentsByProfileIDAsQueryable(int ProfileID, bool CommentStatus, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Comments where w.ProfileID == ProfileID && w.CommentStatus == CommentStatus  orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Comment> GetCommentsByContentIDAsQueryable(int ContentID)
		{
			return (from w in context.Comments where w.ContentID == ContentID orderby w.CommentID descending select w).AsQueryable();
		}

        public IQueryable<Business.DataTransferObjects.Mobile.CommentModel> GetCommentsByContentIDForMobileAsQueryable(int ContentID)
        {
            return (from w in context.Comments where w.ContentID == ContentID && w.CommentStatus == true orderby w.CommentID descending select new Business.DataTransferObjects.Mobile.CommentModel { CommentDateTime=w.CommentDateTime, CommentLike = w.CommentLike, CommentNote = w.CommentNote, ProfileFullname = w.Profile.ProfileFullname, ProfileIsConfirmed = w.Profile.ProfileIsConfirmed, ProfileID = w.ProfileID, CommentID = w.CommentID }).AsQueryable();
        }

        public IQueryable<Comment> GetCommentsByContentIDAsQueryable(int ContentID, bool CommentStatus)
		{
			return (from w in context.Comments where w.ContentID == ContentID && w.CommentStatus == CommentStatus orderby w.CommentID descending select w).AsQueryable();
		}
        public IQueryable<Comment> GetCommentsByContentIDAsQueryable(int ContentID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Comments where w.ContentID == ContentID orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Comment> GetCommentsByContentIDAsQueryable(int ContentID, bool CommentStatus, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Comments where w.ContentID == ContentID && w.CommentStatus == CommentStatus  orderby w.CommentID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

	}
}