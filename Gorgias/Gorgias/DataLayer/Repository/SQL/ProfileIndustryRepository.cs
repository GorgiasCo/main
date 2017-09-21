using System;
using System.Collections.Generic;
using System.Data;
using Gorgias.DataLayer.Interface;
using System.Linq;

namespace Gorgias.DataLayer.Repository.SQL
{   
	public class ProfileIndustryRepository : IProfileIndustryRepository, IDisposable
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
		public bool Insert(int IndustryID, int ProfileID)
		{
            try{
                Profile obj = (from x in context.Profiles where x.ProfileID == ProfileID select x).FirstOrDefault();

                Industry objIndustry = ( from y in context.Industries where y.IndustryID == IndustryID select y).FirstOrDefault();
                obj.Industries.Add(objIndustry);

                context.Profiles.Attach(obj);
    			context.SaveChanges();
                return true;
            }
            catch(Exception ex){
                return false;
            }
		}
		
		public bool Delete(int IndustryID, int ProfileID)
		{			
            Profile obj = (from w in context.Profiles.Include("Industries") where  w.ProfileID == ProfileID select w).FirstOrDefault();
			if (obj != null)
            {
                context.Profiles.Attach(obj);
                obj.Industries.Remove(obj.Industries.Where(m => m.IndustryID == IndustryID).First());                
			    context.SaveChanges();
                return true;
            } else {
                return false;
            }
		}

        public bool Delete(int ProfileID)
        {
            Profile obj = (from w in context.Profiles.Include("Industries") where w.ProfileID == ProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Profiles.Attach(obj);
                obj.Industries.Clear();
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Profile> GetProfileIndustriesByIndustryID(int IndustryID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Profiles.Include("Industries") where w.Industries.Any(m=> m.IndustryID == IndustryID) orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public Profile GetProfileIndustriesByProfileID(int ProfileID)
        {
            return (from w in context.Profiles.Include("Industries") where w.ProfileID == ProfileID orderby w.ProfileID descending select w).FirstOrDefault();
        }
        public IQueryable<Profile> GetProfileIndustriesByIndustryIDAsQueryable(int IndustryID)
		{
			return (from w in context.Profiles.Include("Industries") where w.Industries.Any(m=> m.IndustryID == IndustryID) orderby w.ProfileID descending select w).AsQueryable();
		}
        public IQueryable<Profile> GetProfileIndustriesByIndustryIDAsQueryable(int IndustryID, int page = 1, int pageSize = 7, string filter=null)
        {
            return (from w in context.Profiles.Include("Industries") where w.Industries.Any(m => m.IndustryID == IndustryID) orderby w.ProfileID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

	}
}