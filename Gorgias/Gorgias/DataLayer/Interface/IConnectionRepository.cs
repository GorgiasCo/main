using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{   
        public interface IConnectionRepository
    {
    
    
        Connection Insert(int ProfileID, int RequestedProfileID, int RequestTypeID, Boolean ConnectStatus, DateTime ConnectDateCreated);
        bool Insert(int ProfileID, int RequestedProfileID, int RequestTypeID);
        bool Update(int ProfileID, int RequestedProfileID, int RequestTypeID, Boolean ConnectStatus);        
        bool Delete(int ProfileID, int RequestedProfileID, int RequestTypeID);
        bool Delete(int ProfileID, int RequestedProfileID);
        Connection GetConnection(int ProfileID, int RequestedProfileID, int RequestTypeID);

        //List
        List<Connection> GetConnectionsAll();
        List<Connection> GetConnectionsAll(bool ConnectStatus);
        List<Connection> GetConnectionsAll(int page = 1, int pageSize = 7, string filter=null);
        List<Connection> GetConnectionsAll(bool ConnectionStatus, int page = 1, int pageSize = 7, string filter=null);        
        
        List<Connection> GetConnectionsByProfileID(int ProfileID, bool ConnectStatus);
        List<Connection> GetConnectionsByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        List<Connection> GetConnectionsByProfileID(int ProfileID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter=null);               
        List<Connection> GetConnectionsByRequestedProfileID(int RequestedProfileID, bool ConnectStatus);
        List<Connection> GetConnectionsByRequestedProfileID(int RequestedProfileID, int page = 1, int pageSize = 7, string filter=null);       
        List<Connection> GetConnectionsByRequestedProfileID(int RequestedProfileID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter=null);               
        List<Connection> GetConnectionsByRequestTypeID(int RequestTypeID, bool ConnectStatus);
        List<Connection> GetConnectionsByRequestTypeID(int RequestTypeID, int page = 1, int pageSize = 7, string filter=null);       
        List<Connection> GetConnectionsByRequestTypeID(int RequestTypeID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter=null);               
        
        //IQueryable
        IQueryable<Connection> GetConnectionsAllAsQueryable();
        IQueryable<Connection> GetConnectionsAllAsQueryable(bool ConnectStatus);
        IQueryable<Connection> GetConnectionsAllAsQueryable(int page = 1, int pageSize = 7, string filter=null);
        IQueryable<Connection> GetConnectionsAllAsQueryable(bool ConnectStatus, int page = 1, int pageSize = 7, string filter=null);   
        IQueryable<Connection> GetConnectionsByProfileIDAsQueryable(int ProfileID);
        IQueryable<Connection> GetConnectionsByProfileIDAsQueryable(int ProfileID, bool ConnectStatus);
        IQueryable<Connection> GetConnectionsByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<Connection> GetConnectionsByProfileIDAsQueryable(int ProfileID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter=null);               
        IQueryable<Connection> GetConnectionsByRequestedProfileIDAsQueryable(int RequestedProfileID);
        IQueryable<Connection> GetConnectionsByRequestedProfileIDAsQueryable(int RequestedProfileID, bool ConnectStatus);
        IQueryable<Connection> GetConnectionsByRequestedProfileIDAsQueryable(int RequestedProfileID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<Connection> GetConnectionsByRequestedProfileIDAsQueryable(int RequestedProfileID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter=null);               
        IQueryable<Connection> GetConnectionsByRequestTypeIDAsQueryable(int RequestTypeID);
        IQueryable<Connection> GetConnectionsByRequestTypeIDAsQueryable(int RequestTypeID, bool ConnectStatus);
        IQueryable<Connection> GetConnectionsByRequestTypeIDAsQueryable(int RequestTypeID, int page = 1, int pageSize = 7, string filter=null);       
        IQueryable<Connection> GetConnectionsByRequestTypeIDAsQueryable(int RequestTypeID, bool ConnectStatus, int page = 1, int pageSize = 7, string filter=null);               
    }
}


