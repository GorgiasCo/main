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
    public class ConnectionRepository : IConnectionRepository, IDisposable
    {
        // To detect redundant calls
        private bool disposedValue = false;

        private GorgiasEntities context = new GorgiasEntities();

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
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
        public Connection Insert(int ProfileID, int RequestedProfileID, int RequestTypeID, Boolean ConnectStatus, DateTime ConnectDateCreated)
        {
            try
            {
                Connection obj = new Connection();
                obj.ProfileID = ProfileID;
                obj.RequestedProfileID = RequestedProfileID;
                obj.RequestTypeID = RequestTypeID;
                obj.ConnectStatus = ConnectStatus;
                obj.ConnectDateCreated = DateTime.Now;
                context.Connections.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                return new Connection();
            }
        }

        public bool Insert(int ProfileID, int RequestedProfileID, int RequestTypeID)
        {
            var currentConnection = GetConnection(ProfileID, RequestedProfileID, RequestTypeID);
            if (currentConnection != null)
            {
                Delete(ProfileID, RequestedProfileID, RequestTypeID);
                return false;
            }
            else
            {
                try
                {
                    Connection obj = new Connection();
                    obj.ProfileID = ProfileID;
                    obj.RequestedProfileID = RequestedProfileID;
                    obj.RequestTypeID = RequestTypeID;
                    obj.ConnectStatus = true;
                    obj.ConnectDateCreated = DateTime.UtcNow;
                    context.Connections.Add(obj);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Insert(int ProfileID, int RequestedProfileID, int RequestTypeID, bool ConnectionStatus)
        {
            Connection resultStayON = (from w in context.Connections where w.ProfileID == ProfileID && w.RequestedProfileID == RequestedProfileID && w.RequestTypeID == 3 select w).FirstOrDefault();
            if(resultStayON != null)
            {
                try
                {
                    Connection obj = new Connection();
                    obj.ProfileID = ProfileID;
                    obj.RequestedProfileID = RequestedProfileID;
                    obj.RequestTypeID = RequestTypeID;
                    obj.ConnectStatus = ConnectionStatus;
                    obj.ConnectDateCreated = DateTime.UtcNow;

                    context.Connections.Add(obj);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    Connection obj = new Connection();
                    obj.ProfileID = ProfileID;
                    obj.RequestedProfileID = RequestedProfileID;
                    obj.RequestTypeID = RequestTypeID;
                    obj.ConnectStatus = ConnectionStatus;
                    obj.ConnectDateCreated = DateTime.UtcNow;

                    Connection objStayOn = new Connection();
                    objStayOn.ProfileID = ProfileID;
                    objStayOn.RequestedProfileID = RequestedProfileID;
                    objStayOn.RequestTypeID = 3;
                    objStayOn.ConnectStatus = ConnectionStatus;
                    objStayOn.ConnectDateCreated = DateTime.UtcNow;

                    context.Connections.Add(obj);
                    context.Connections.Add(objStayOn);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool Update(int ProfileID, int RequestedProfileID, int RequestTypeID, Boolean ConnectStatus)
        {
            Connection obj = new Connection();
            obj = (from w in context.Connections where w.ProfileID == RequestedProfileID && w.RequestedProfileID == ProfileID && w.RequestTypeID == RequestTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Connections.Attach(obj);
                obj.ConnectStatus = ConnectStatus;
                obj.ConnectDateCreated = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ProfileID, int RequestedProfileID)
        {
            Connection obj = new Connection();
            obj = (from w in context.Connections where w.ProfileID == ProfileID && w.RequestedProfileID == RequestedProfileID && w.RequestTypeID == 4 && w.ConnectStatus == false select w).FirstOrDefault();
            if (obj != null)
            {
                context.Connections.Attach(obj);
                obj.ConnectStatus = true;                
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ProfileID, int RequestedProfileID)
        {
            Connection obj = new Connection();
            obj = (from w in context.Connections where w.ProfileID == ProfileID && w.RequestedProfileID == RequestedProfileID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Connections.Attach(obj);
                context.Connections.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int ProfileID, int RequestedProfileID, int RequestTypeID)
        {
            Connection obj = new Connection();
            obj = (from w in context.Connections where w.ProfileID == ProfileID && w.RequestedProfileID == RequestedProfileID && w.RequestTypeID == RequestTypeID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Connections.Attach(obj);
                context.Connections.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Connection GetConnection(int ProfileID, int RequestedProfileID, int RequestTypeID)
        {
            return (from w in context.Connections where w.ProfileID == ProfileID && w.RequestedProfileID == RequestedProfileID && w.RequestTypeID == RequestTypeID select w).FirstOrDefault();
        }

        //Lists
        public List<Connection> GetConnectionsAll()
        {
            return (from w in context.Connections orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).ToList();
        }
        public List<Connection> GetConnectionsAll(bool ConnectStatus)
        {
            return (from w in context.Connections where w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).ToList();
        }
        //List Pagings
        public List<Connection> GetConnectionsAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Connection>();
            if (filter != null)
            {
                xList = (from w in context.Connections orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Connections orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Connection> GetConnectionsAll(bool ConnectStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Connection>();
            if (filter != null)
            {
                xList = (from w in context.Connections where w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Connections where w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Connection> GetConnectionsAllAsQueryable()
        {
            return (from w in context.Connections.Include("Profile").Include("Profile1") orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsAllAsQueryable(bool ConnectStatus)
        {
            return (from w in context.Connections where w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).AsQueryable();
        }
        //IQueryable Pagings

        //V2 Begin ;)
        //ProfileSubscribeMobileModel
        public IQueryable<Business.DataTransferObjects.Mobile.V2.ProfileSubscribeMobileModel> GetConnectionsByProfileIDAllAsQueryable(int ProfileID)
        {
            return context.Connections.Where(m => m.RequestedProfileID == ProfileID && m.RequestTypeID == 3).Select(c => new Business.DataTransferObjects.Mobile.V2.ProfileSubscribeMobileModel { ProfileID = c.ProfileID }).AsQueryable();
        }
        //V2 End ;)

        public IQueryable<Connection> GetConnectionsAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Connection> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Connections orderby w.ConnectionID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Connections.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Connections orderby w.ConnectionID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Connections.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Connection> GetConnectionsAllAsQueryable(bool ConnectStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Connection> xList;
            if (filter != null)
            {
                xList = (from w in context.Connections where w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Connections where w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Connection> GetConnectionsByProfileID(int ProfileID, bool ConnectStatus)
        {
            return (from w in context.Connections where w.ProfileID == ProfileID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).ToList();
        }
        public List<Connection> GetConnectionsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.ProfileID == ProfileID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Connection> GetConnectionsByProfileID(int ProfileID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.ProfileID == ProfileID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Connection> GetConnectionsByRequestedProfileID(int RequestedProfileID, bool ConnectStatus)
        {
            return (from w in context.Connections where w.RequestedProfileID == RequestedProfileID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).ToList();
        }
        public List<Connection> GetConnectionsByRequestedProfileID(int RequestedProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.RequestedProfileID == RequestedProfileID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Connection> GetConnectionsByRequestedProfileID(int RequestedProfileID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.RequestedProfileID == RequestedProfileID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Connection> GetConnectionsByRequestTypeID(int RequestTypeID, bool ConnectStatus)
        {
            return (from w in context.Connections where w.RequestTypeID == RequestTypeID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).ToList();
        }
        public List<Connection> GetConnectionsByRequestTypeID(int RequestTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.RequestTypeID == RequestTypeID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Connection> GetConnectionsByRequestTypeID(int RequestTypeID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.RequestTypeID == RequestTypeID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Connection> GetConnectionsByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.Connections where w.ProfileID == ProfileID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByProfileIDAsQueryable(int ProfileID, bool ConnectStatus)
        {
            return (from w in context.Connections where w.ProfileID == ProfileID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.ProfileID == ProfileID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByProfileIDAsQueryable(int ProfileID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.ProfileID == ProfileID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByRequestedProfileIDAsQueryable(int RequestedProfileID)
        {
            return (from w in context.Connections where w.RequestedProfileID == RequestedProfileID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByRequestedProfileIDAsQueryable(int RequestedProfileID, bool ConnectStatus)
        {
            return (from w in context.Connections.Include("Profile").Include("Profile1") where w.RequestedProfileID == RequestedProfileID && w.RequestTypeID == 2 orderby w.ConnectStatus ascending select w).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByRequestedProfileIDAsQueryable(int RequestedProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.RequestedProfileID == RequestedProfileID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByRequestedProfileIDAsQueryable(int RequestedProfileID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.RequestedProfileID == RequestedProfileID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByRequestTypeIDAsQueryable(int RequestTypeID)
        {
            return (from w in context.Connections where w.RequestTypeID == RequestTypeID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByRequestTypeIDAsQueryable(int RequestTypeID, bool ConnectStatus)
        {
            return (from w in context.Connections where w.RequestTypeID == RequestTypeID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByRequestTypeIDAsQueryable(int RequestTypeID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.RequestTypeID == RequestTypeID orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Connection> GetConnectionsByRequestTypeIDAsQueryable(int RequestTypeID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Connections where w.RequestTypeID == RequestTypeID && w.ConnectStatus == ConnectStatus orderby w.ProfileID, w.RequestedProfileID, w.RequestTypeID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}