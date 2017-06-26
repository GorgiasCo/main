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
	public class UserRoleRepository : IUserRoleRepository, IDisposable
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
		public UserRole Insert(String UserRoleName, Boolean UserRoleStatus, String UserRoleImage, String UserRoleDescription)
		{
            try{
                UserRole obj = new UserRole();	
                
                
            		obj.UserRoleName = UserRoleName;			
            		obj.UserRoleStatus = UserRoleStatus;			
            		obj.UserRoleImage = UserRoleImage;			
            		obj.UserRoleDescription = UserRoleDescription;			
    			context.UserRoles.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new UserRole();
            }
		}

		public bool Update(int UserRoleID, String UserRoleName, Boolean UserRoleStatus, String UserRoleImage, String UserRoleDescription)
		{
		    UserRole obj = new UserRole();
            obj = (from w in context.UserRoles where w.UserRoleID == UserRoleID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.UserRoles.Attach(obj);
	
        		obj.UserRoleName = UserRoleName;			
        		obj.UserRoleStatus = UserRoleStatus;			
        		obj.UserRoleImage = UserRoleImage;			
        		obj.UserRoleDescription = UserRoleDescription;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int UserRoleID)
		{
			UserRole obj = new UserRole();
			obj = (from w in context.UserRoles where  w.UserRoleID == UserRoleID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.UserRoles.Attach(obj);
			    context.UserRoles.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public UserRole GetUserRole(int UserRoleID)
		{
			return (from w in context.UserRoles where  w.UserRoleID == UserRoleID  select w).FirstOrDefault();
		}

        //Lists
		public List<UserRole> GetUserRolesAll()
		{
			return (from w in context.UserRoles orderby w.UserRoleID descending select w).ToList();
		}
		public List<UserRole> GetUserRolesAll(bool UserRoleStatus)
		{
			return (from w in context.UserRoles where w.UserRoleStatus == UserRoleStatus orderby w.UserRoleID descending select w).ToList();
		}
        //List Pagings
        public List<UserRole> GetUserRolesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<UserRole>();
            if (filter != null)
            {
                xList = (from w in context.UserRoles orderby w.UserRoleID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.UserRoles orderby w.UserRoleID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<UserRole> GetUserRolesAll(bool UserRoleStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<UserRole>();
            if (filter != null)
            {
                xList = (from w in context.UserRoles where w.UserRoleStatus == UserRoleStatus  orderby w.UserRoleID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.UserRoles where w.UserRoleStatus == UserRoleStatus orderby w.UserRoleID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<UserRole> GetUserRolesAllAsQueryable()
		{
			return (from w in context.UserRoles orderby w.UserRoleID descending select w).AsQueryable();
		}
		public IQueryable<UserRole> GetUserRolesAllAsQueryable(bool UserRoleStatus)
		{
			return (from w in context.UserRoles where w.UserRoleStatus == UserRoleStatus orderby w.UserRoleID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<UserRole> GetUserRolesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<UserRole> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.UserRoles orderby w.UserRoleID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.UserRoles.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.UserRoles orderby w.UserRoleID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.UserRoles.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<UserRole> GetUserRolesAllAsQueryable(bool UserRoleStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<UserRole> xList;
            if (filter != null)
            {
                xList = (from w in context.UserRoles where w.UserRoleStatus == UserRoleStatus orderby w.UserRoleID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.UserRoles where w.UserRoleStatus == UserRoleStatus orderby w.UserRoleID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}