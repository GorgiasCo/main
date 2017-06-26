using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Gorgias.DataLayer.Repository;
using Gorgias.Business.DataTransferObjects;
using Gorgias.Infrastruture.Core;
using Gorgias.Business.DataTransferObjects.Helper;
using EntityFramework.Extensions;

namespace Gorgias.BusinessLayer.Facades
{   
    public class MessageFacade
    {                
        public MessageDTO GetMessage(int MessageID)
        {
            MessageDTO result = Mapper.Map<MessageDTO>(DataLayer.DataLayerFacade.MessageRepository().GetMessage(MessageID));             
            return result;
        }

        public DTResult<MessageDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.MessageRepository().GetMessagesAllAsQueryable();

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower()) || p.Profile1.ProfileFullname.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Message>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<MessageDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<MessageDTO> result = new DTResult<MessageDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }       

        public PaginationSet<MessageDTO> GetMessages(int page, int pagesize)
        {           
            var basequery = DataLayer.DataLayerFacade.MessageRepository().GetMessagesAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Message>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<MessageDTO> result = new PaginationSet<MessageDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<MessageDTO>>(queryList.ToList())
            };

            return result;            
        }
        
        public List<MessageDTO> GetMessages()
        {           
            var basequery = Mapper.Map <List<MessageDTO>>(DataLayer.DataLayerFacade.MessageRepository().GetMessagesAllAsQueryable());
            return basequery.ToList();
        }

        
        public DTResult<MessageDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.MessageRepository().GetMessagesAllAsQueryable().Where(m=> m.ProfileID==ProfileID);

            if (search.Length>0) {
                basequery = basequery.Where(p => (p.MessageNote.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Message>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<MessageDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<MessageDTO> result = new DTResult<MessageDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }               
        
        
        public PaginationSet<MessageDTO> GetMessagesByProfileID(int ProfileID, int page, int pagesize)
        {
            
            var basequery = DataLayer.DataLayerFacade.MessageRepository().GetMessagesByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Message>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<MessageDTO> result = new PaginationSet<MessageDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<MessageDTO>>(queryList.ToList())
            };

            return result;            
        }

        public MessageDTO Insert(MessageDTO objMessage)
        {            
            MessageDTO result = Mapper.Map<MessageDTO>(DataLayer.DataLayerFacade.MessageRepository().Insert(objMessage.MessageNote, objMessage.MessageDateCreated, objMessage.MessageSubject, objMessage.MessageStatus, objMessage.MessageIsDeleted, objMessage.ProfileID));

            if (result!=null)
            {
                return result;            
            }
            else
            {
                return new MessageDTO();
            }
        }
               
        public bool Delete(int MessageID)
        {            
            bool result = DataLayer.DataLayerFacade.MessageRepository().Delete(MessageID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }

        public bool Update(int MessageID, MessageDTO objMessage)
        {            
            bool result = DataLayer.DataLayerFacade.MessageRepository().Update(objMessage.MessageID, objMessage.MessageNote, objMessage.MessageDateCreated, objMessage.MessageSubject, objMessage.MessageStatus, objMessage.MessageIsDeleted, objMessage.ProfileID);
            if (result)
            {
                return true;            
            }
            else
            {
                return false;
            }
        }
    }
}