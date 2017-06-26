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
    public class MessageRepository : IMessageRepository, IDisposable
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
        public Message Insert(String MessageNote, DateTime MessageDateCreated, String MessageSubject, Boolean MessageStatus, Boolean MessageIsDeleted, int ProfileID)
        {
            try
            {
                Message obj = new Message();
                obj.MessageNote = MessageNote;
                obj.MessageDateCreated = MessageDateCreated;
                obj.MessageSubject = MessageSubject;
                obj.MessageStatus = MessageStatus;
                obj.MessageIsDeleted = MessageIsDeleted;
                obj.ProfileID = ProfileID;
                context.Messages.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException();
            }
        }

        public Message Insert(String MessageNote, DateTime MessageDateCreated, String MessageSubject, Boolean MessageStatus, Boolean MessageIsDeleted, int ProfileID, int RequestedProfileID)
        {
            try
            {
                Message obj = new Message();
                obj.MessageNote = MessageNote;
                obj.MessageDateCreated = MessageDateCreated;
                obj.MessageSubject = MessageSubject;
                obj.MessageStatus = MessageStatus;
                obj.MessageIsDeleted = MessageIsDeleted;
                obj.ProfileID = RequestedProfileID;
                obj.Profile1 = (from ex in context.Profiles where ex.ProfileID == ProfileID select ex).First();
                context.Messages.Add(obj);
                context.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException();
            }
        }

        public bool Update(int MessageID, String MessageNote, DateTime MessageDateCreated, String MessageSubject, Boolean MessageStatus, Boolean MessageIsDeleted, int ProfileID)
        {
            Message obj = new Message();
            obj = (from w in context.Messages where w.MessageID == MessageID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Messages.Attach(obj);

                obj.MessageNote = MessageNote;
                obj.MessageDateCreated = MessageDateCreated;
                obj.MessageSubject = MessageSubject;
                obj.MessageStatus = MessageStatus;
                obj.MessageIsDeleted = MessageIsDeleted;
                obj.ProfileID = ProfileID;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int MessageID)
        {
            Message obj = new Message();
            obj = (from w in context.Messages where w.MessageID == MessageID select w).FirstOrDefault();
            if (obj != null)
            {
                context.Messages.Attach(obj);
                context.Messages.Remove(obj);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Message GetMessage(int MessageID)
        {
            return (from w in context.Messages where w.MessageID == MessageID select w).FirstOrDefault();
        }

        //Lists
        public List<Message> GetMessagesAll()
        {
            return (from w in context.Messages orderby w.MessageID descending select w).ToList();
        }
        public List<Message> GetMessagesAll(bool MessageStatus)
        {
            return (from w in context.Messages where w.MessageStatus == MessageStatus orderby w.MessageID descending select w).ToList();
        }
        //List Pagings
        public List<Message> GetMessagesAll(int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Message>();
            if (filter != null)
            {
                xList = (from w in context.Messages orderby w.MessageID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Messages orderby w.MessageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        public List<Message> GetMessagesAll(bool MessageStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            var xList = new List<Message>();
            if (filter != null)
            {
                xList = (from w in context.Messages where w.MessageStatus == MessageStatus orderby w.MessageID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else
            {
                xList = (from w in context.Messages where w.MessageStatus == MessageStatus orderby w.MessageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            return xList;
        }
        //IQueryable
        public IQueryable<Message> GetMessagesAllAsQueryable()
        {
            return (from w in context.Messages.Include("Profile").Include("Profile1") orderby w.MessageID descending select w).AsQueryable();
        }
        public IQueryable<Message> GetMessagesAllAsQueryable(bool MessageStatus)
        {
            return (from w in context.Messages where w.MessageStatus == MessageStatus orderby w.MessageID descending select w).AsQueryable();
        }
        //IQueryable Pagings
        public IQueryable<Message> GetMessagesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Message> xList;
            if (filter != null)
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Messages orderby w.MessageID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Messages.ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                //In Case of Performance, Use following method ;)
                //xList = (from w in context.Messages orderby w.MessageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();                
                xList = context.Messages.Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        public IQueryable<Message> GetMessagesAllAsQueryable(bool MessageStatus = true, int page = 1, int pageSize = 7, string filter = null)
        {
            IQueryable<Message> xList;
            if (filter != null)
            {
                xList = (from w in context.Messages where w.MessageStatus == MessageStatus orderby w.MessageID descending select w).ApplySort(filter).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            else
            {
                xList = (from w in context.Messages where w.MessageStatus == MessageStatus orderby w.MessageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
            }
            return xList;
        }
        //Relationship Functions based on Foreign Key
        //Relationship List
        public List<Message> GetMessagesByProfileID(int ProfileID, bool MessageStatus)
        {
            return (from w in context.Messages where w.ProfileID == ProfileID && w.MessageStatus == MessageStatus orderby w.MessageID descending select w).ToList();
        }
        public List<Message> GetMessagesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Messages where w.ProfileID == ProfileID orderby w.MessageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public List<Message> GetMessagesByProfileID(int ProfileID, bool MessageStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Messages where w.ProfileID == ProfileID && w.MessageStatus == MessageStatus orderby w.MessageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        }
        public IQueryable<Message> GetMessagesByProfileIDAsQueryable(int ProfileID)
        {
            return (from w in context.Messages where w.ProfileID == ProfileID orderby w.MessageID descending select w).AsQueryable();
        }
        public IQueryable<Message> GetMessagesByProfileIDAsQueryable(int ProfileID, bool MessageStatus)
        {
            return (from w in context.Messages where w.ProfileID == ProfileID && w.MessageStatus == MessageStatus orderby w.MessageID descending select w).AsQueryable();
        }
        public IQueryable<Message> GetMessagesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Messages where w.ProfileID == ProfileID orderby w.MessageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }
        public IQueryable<Message> GetMessagesByProfileIDAsQueryable(int ProfileID, bool MessageStatus, int page = 1, int pageSize = 7, string filter = null)
        {
            return (from w in context.Messages where w.ProfileID == ProfileID && w.MessageStatus == MessageStatus orderby w.MessageID descending select w).Skip(pageSize * (page - 1)).Take(pageSize).AsQueryable();
        }

    }
}