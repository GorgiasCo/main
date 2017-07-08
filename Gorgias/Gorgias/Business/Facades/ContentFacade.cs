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
    public class ContentFacade
    {
        public ContentDTO GetContent(int ContentID)
        {
            ContentDTO result = Mapper.Map<ContentDTO>(DataLayer.DataLayerFacade.ContentRepository().GetContent(ContentID));
            return result;
        }

        public object getAlbumContentSet(int ProfileID) {
            return DataLayer.DataLayerFacade.AlbumRepository().getAlbumSet(ProfileID);
        }

        public DTResult<ContentDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.ContentRepository().GetContentsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ContentTitle.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Content>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ContentDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ContentDTO> result = new DTResult<ContentDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<ContentDTO> GetContents(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.ContentRepository().GetContentsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Content>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ContentDTO> result = new PaginationSet<ContentDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ContentDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<ContentDTO> GetContents()
        {
            var basequery = Mapper.Map<List<ContentDTO>>(DataLayer.DataLayerFacade.ContentRepository().GetContentsAllAsQueryable());
            return basequery.ToList();
        }


        public DTResult<ContentDTO> FilterResultByAlbumID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int AlbumID)
        {
            var basequery = DataLayer.DataLayerFacade.ContentRepository().GetContentsAllAsQueryable().Where(m => m.AlbumID == AlbumID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.ContentTitle.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Content>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<ContentDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<ContentDTO> result = new DTResult<ContentDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }


        public PaginationSet<ContentDTO> GetContentsByAlbumID(int AlbumID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.ContentRepository().GetContentsByAlbumIDAsQueryable(AlbumID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Content>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<ContentDTO> result = new PaginationSet<ContentDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<ContentDTO>>(queryList.ToList())
            };

            return result;
        }

        public ContentDTO Insert(ContentDTO objContent)
        {
            ContentDTO result = Mapper.Map<ContentDTO>(DataLayer.DataLayerFacade.ContentRepository().Insert(objContent.ContentTitle, objContent.ContentURL, objContent.ContentType, objContent.ContentStatus, objContent.ContentIsDeleted, objContent.ContentCreatedDate, objContent.AlbumID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ContentDTO();
            }
        }

        public bool Insert(List<Business.DataTransferObjects.Mobile.ContentUploadMobileModel> objContents, int AlbumID)
        {
            foreach(Business.DataTransferObjects.Mobile.ContentUploadMobileModel objContent in objContents)
            {
                ContentDTO obj = new ContentDTO();
                obj.AlbumID = AlbumID;
                obj.ContentIsDeleted = false;
                obj.ContentStatus = true;
                //obj.ContentTitle = objContent.ContentTitle;
                if (objContent.ContentTitle == string.Empty)
                {
                    obj.ContentTitle = null;
                }
                else
                {
                    obj.ContentTitle = objContent.ContentTitle;
                }
                obj.ContentType = 1;
                obj.ContentCreatedDate = DateTime.UtcNow;
                // System.Configuration.ConfigurationManager.AppSettings["ContentCDN"] Can be used for CDN main Path
                obj.ContentURL = objContent.ContentURL;
                Insert(obj);
            }
            return true; 
        }

        public AlbumDTO Insert(List<Business.DataTransferObjects.Mobile.ContentUploadMobileModel> objContents, int Availability, int CategoryID, int ProfileID)
        {
            AlbumDTO objAlbum = new AlbumDTO();
            objAlbum.AlbumCover = objContents.First().ContentURL;
            objAlbum.AlbumName = objContents.First().ContentTitle;
            objAlbum.AlbumDateCreated = DateTime.UtcNow;
            objAlbum.AlbumDateExpires = objAlbum.AlbumDateCreated;
            objAlbum.AlbumDatePublish = objAlbum.AlbumDateCreated;
            objAlbum.AlbumIsDeleted = false;
            objAlbum.AlbumStatus = true;
            objAlbum.AlbumView = 0;            
            objAlbum.CategoryID = CategoryID;
            objAlbum.ProfileID = ProfileID;
            objAlbum.AlbumAvailability = Availability;
            objAlbum.AlbumHasComment = true;

            AlbumDTO result = Facade.AlbumFacade().InsertForMobile(objAlbum);
            if(result.AlbumID > 0)
            {
                //To add album cover as independent image in album to get likes ;) like Facebook ;)
                //objContents.RemoveAt(0);
                //We reverse the list
                objContents.Reverse();
                foreach (Business.DataTransferObjects.Mobile.ContentUploadMobileModel objContent in objContents)
                {
                    ContentDTO obj = new ContentDTO();
                    obj.AlbumID = result.AlbumID;
                    obj.ContentIsDeleted = false;
                    obj.ContentStatus = true;
                    if(objContent.ContentTitle == string.Empty)
                    {
                        obj.ContentTitle = null;
                    } else
                    {
                        obj.ContentTitle = objContent.ContentTitle;
                    }                   
                    obj.ContentType = 1;
                    obj.ContentCreatedDate = DateTime.UtcNow;
                    // System.Configuration.ConfigurationManager.AppSettings["ContentCDN"] Can be used for CDN main Path
                    obj.ContentURL = objContent.ContentURL;
                    Insert(obj);
                }
            }
            return result;
        }

        public AlbumDTO Insert(List<Business.DataTransferObjects.Mobile.ContentUploadMobileModel> objContents, int Availability, int CategoryID, int ProfileID, bool AlbumHasComment)
        {
            AlbumDTO objAlbum = new AlbumDTO();
            objAlbum.AlbumCover = objContents.First().ContentURL;
            objAlbum.AlbumName = objContents.First().ContentTitle;
            objAlbum.AlbumDateCreated = DateTime.UtcNow;
            objAlbum.AlbumDateExpires = objAlbum.AlbumDateCreated;
            objAlbum.AlbumDatePublish = objAlbum.AlbumDateCreated;
            objAlbum.AlbumHasComment = AlbumHasComment;
            objAlbum.AlbumIsDeleted = false;
            objAlbum.AlbumStatus = true;
            objAlbum.AlbumView = 0;
            objAlbum.CategoryID = CategoryID;
            objAlbum.ProfileID = ProfileID;
            objAlbum.AlbumAvailability = Availability;
            //objAlbum.AlbumHasComment = true;

            AlbumDTO result = Facade.AlbumFacade().InsertForMobile(objAlbum);
            if (result.AlbumID > 0)
            {
                //To add album cover as independent image in album to get likes ;) like Facebook ;)
                //objContents.RemoveAt(0);
                //We reverse the list
                objContents.Reverse();
                foreach (Business.DataTransferObjects.Mobile.ContentUploadMobileModel objContent in objContents)
                {
                    ContentDTO obj = new ContentDTO();
                    obj.AlbumID = result.AlbumID;
                    obj.ContentIsDeleted = false;
                    obj.ContentStatus = true;
                    if (objContent.ContentTitle == string.Empty)
                    {
                        obj.ContentTitle = null;
                    }
                    else
                    {
                        obj.ContentTitle = objContent.ContentTitle;
                    }
                    obj.ContentType = 1;
                    obj.ContentCreatedDate = DateTime.UtcNow;
                    // System.Configuration.ConfigurationManager.AppSettings["ContentCDN"] Can be used for CDN main Path
                    obj.ContentURL = objContent.ContentURL;
                    Insert(obj);
                }
            }
            return result;
        }

        public bool Delete(int ContentID)
        {
            bool result = DataLayer.DataLayerFacade.ContentRepository().Delete(ContentID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int ContentID, ContentDTO objContent)
        {
            bool result = DataLayer.DataLayerFacade.ContentRepository().Update(objContent.ContentID, objContent.ContentTitle, objContent.ContentURL, objContent.ContentType, objContent.ContentStatus, objContent.ContentIsDeleted, objContent.AlbumID);
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