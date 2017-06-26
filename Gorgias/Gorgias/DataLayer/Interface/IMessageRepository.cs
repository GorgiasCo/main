using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
namespace Gorgias.DataLayer.Interface
{
    public interface IMessageRepository
    {
        Message Insert(String MessageNote, DateTime MessageDateCreated, String MessageSubject, Boolean MessageStatus, Boolean MessageIsDeleted, int ProfileID);
        Message Insert(String MessageNote, DateTime MessageDateCreated, String MessageSubject, Boolean MessageStatus, Boolean MessageIsDeleted, int ProfileID, int RequestedProfileID);
        bool Update(int MessageID, String MessageNote, DateTime MessageDateCreated, String MessageSubject, Boolean MessageStatus, Boolean MessageIsDeleted, int ProfileID);
        bool Delete(int MessageID);

        Message GetMessage(int MessageID);

        //List
        List<Message> GetMessagesAll();
        List<Message> GetMessagesAll(bool MessageStatus);
        List<Message> GetMessagesAll(int page = 1, int pageSize = 7, string filter = null);
        List<Message> GetMessagesAll(bool MessageStatus, int page = 1, int pageSize = 7, string filter = null);

        List<Message> GetMessagesByProfileID(int ProfileID, bool MessageStatus);
        List<Message> GetMessagesByProfileID(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
        List<Message> GetMessagesByProfileID(int ProfileID, bool MessageStatus, int page = 1, int pageSize = 7, string filter = null);

        //IQueryable
        IQueryable<Message> GetMessagesAllAsQueryable();
        IQueryable<Message> GetMessagesAllAsQueryable(bool MessageStatus);
        IQueryable<Message> GetMessagesAllAsQueryable(int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Message> GetMessagesAllAsQueryable(bool MessageStatus, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Message> GetMessagesByProfileIDAsQueryable(int ProfileID);
        IQueryable<Message> GetMessagesByProfileIDAsQueryable(int ProfileID, bool MessageStatus);
        IQueryable<Message> GetMessagesByProfileIDAsQueryable(int ProfileID, int page = 1, int pageSize = 7, string filter = null);
        IQueryable<Message> GetMessagesByProfileIDAsQueryable(int ProfileID, bool MessageStatus, int page = 1, int pageSize = 7, string filter = null);
    }
}


