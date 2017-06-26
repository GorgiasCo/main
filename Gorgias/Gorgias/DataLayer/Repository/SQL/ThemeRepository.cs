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
	public class ThemeRepository : IThemeRepository, IDisposable
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
		public Theme Insert(String ThemeName, String ThemeClassCode, Boolean ThemeStatus)
		{
            try{
                Theme obj = new Theme();	
                
                
            		obj.ThemeName = ThemeName;			
            		obj.ThemeClassCode = ThemeClassCode;			
            		obj.ThemeStatus = ThemeStatus;			
    			context.Themes.Add(obj);
    			context.SaveChanges();
                return obj;
            }
            catch(Exception ex){
                return new Theme();
            }
		}

		public bool Update(int ThemeID, String ThemeName, String ThemeClassCode, Boolean ThemeStatus)
		{
		    Theme obj = new Theme();
            obj = (from w in context.Themes where w.ThemeID == ThemeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Themes.Attach(obj);
	
        		obj.ThemeName = ThemeName;			
        		obj.ThemeClassCode = ThemeClassCode;			
        		obj.ThemeStatus = ThemeStatus;			
			context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public bool Delete(int ThemeID)
		{
			Theme obj = new Theme();
			obj = (from w in context.Themes where  w.ThemeID == ThemeID  select w).FirstOrDefault();
			if (obj != null)
            {
                context.Themes.Attach(obj);
			    context.Themes.Remove(obj);
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

		public Theme GetTheme(int ThemeID)
		{
			return (from w in context.Themes where  w.ThemeID == ThemeID  select w).FirstOrDefault();
		}

        //Lists
		public List<Theme> GetThemesAll()
		{
			return (from w in context.Themes orderby w.ThemeID descending select w).ToList();
		}
		public List<Theme> GetThemesAll(bool ThemeStatus)
		{
			return (from w in context.Themes where w.ThemeStatus == ThemeStatus orderby w.ThemeID descending select w).ToList();
		}
        //List Pagings
        public List<Theme> GetThemesAll(int page = 1, int pageSize = 7, string filter=null)
		{
            var xList = new List<Theme>();
            if (filter != null)
            {
                xList = (from w in context.Themes orderby w.ThemeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Themes orderby w.ThemeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList;     
		}
		public List<Theme> GetThemesAll(bool ThemeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			            var xList = new List<Theme>();
            if (filter != null)
            {
                xList = (from w in context.Themes where w.ThemeStatus == ThemeStatus  orderby w.ThemeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            else {
                xList = (from w in context.Themes where w.ThemeStatus == ThemeStatus orderby w.ThemeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();                
            }
            return xList; 
		}
        //IQueryable
		public IQueryable<Theme> GetThemesAllAsQueryable()
		{
			return (from w in context.Themes orderby w.ThemeID descending select w).AsQueryable();
		}
		public IQueryable<Theme> GetThemesAllAsQueryable(bool ThemeStatus)
		{
			return (from w in context.Themes where w.ThemeStatus == ThemeStatus orderby w.ThemeID descending select w).AsQueryable();
		}
        //IQueryable Pagings
        public IQueryable<Theme> GetThemesAllAsQueryable(int page = 1, int pageSize = 7, string filter=null)
		{
            IQueryable<Theme> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Themes orderby w.ThemeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Themes.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Themes orderby w.ThemeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList= context.Themes.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;     
		}
		public IQueryable<Theme> GetThemesAllAsQueryable(bool ThemeStatus = true, int page = 1, int pageSize = 7, string filter=null)
		{
			IQueryable<Theme> xList;
            if (filter != null)
            {
                xList = (from w in context.Themes where w.ThemeStatus == ThemeStatus orderby w.ThemeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            else {
                xList = (from w in context.Themes where w.ThemeStatus == ThemeStatus orderby w.ThemeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
            }
            return xList; 
		}

	}
}