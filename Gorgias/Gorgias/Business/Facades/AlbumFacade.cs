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
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;

namespace Gorgias.BusinessLayer.Facades
{
    public class AlbumFacade
    {
        //V2 Begin
        public int? getAlbumTotalViewsByProfileID(int ProfileID)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().GetAlbumViewsV2Mobile(ProfileID);
        }

        public PaginationSet<Business.DataTransferObjects.Mobile.V2.AlbumMobileModel> getAlbums(Business.DataTransferObjects.Mobile.V2.AlbumFilterMobileModel albumFilterMobileModel)
        {
            IQueryable<Album> basequery;
            //if (albumFilterMobileModel.CategoryTypeID == 2 && albumFilterMobileModel.isMicroApp == false)
            if (albumFilterMobileModel.CategoryTypeID == 2)
            {
                basequery = getAlbumsByCategoryAction(albumFilterMobileModel);
            }
            else
            {
                basequery = getAlbumsByCategoryContent(albumFilterMobileModel);
            }

            //int pageRequested = 0;

            //if(albumFilterMobileModel.Page > 2)
            //{
            //    pageRequested = albumFilterMobileModel.Page - 2;
            //} else
            //{
            //    pageRequested = albumFilterMobileModel.Page;
            //}

            var queryList = RepositoryHelper.Pagination<Album>(albumFilterMobileModel.Page, albumFilterMobileModel.Size, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            var tt = queryList.Select(w => new Business.DataTransferObjects.Mobile.V2.AlbumMobileModel
            {
                ProfileID = w.ProfileID,
                AlbumID = w.AlbumID,
                AlbumCover = w.AlbumCover,
                AlbumDateCreated = w.AlbumDateCreated,
                AlbumName = w.AlbumName,
                AlbumAvailability = w.AlbumAvailability,
                AlbumDateExpire = w.AlbumDateExpire,
                AlbumDatePublish = w.AlbumDatePublish,
                //AlbumAvailabilityName = w.AlbumType.AlbumTypeName,
                AlbumLike = w.Contents.Sum(m => m.ContentLike),
                AlbumContents = w.Contents.Count,
                AlbumComments = w.Contents.Sum(m => m.Comments.Count),
                AlbumHasComment = w.AlbumHasComment,
                AlbumIsTokenAvailable = w.AlbumIsTokenAvailable,
                CategoryID = w.CategoryID,
                AlbumRepostRequest = w.AlbumRepostRequest,
                AlbumRepostValue = w.AlbumRepostValue,
                //Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c => new Business.DataTransferObjects.Mobile.V2.ContentMobileModel()
                //{
                //    ContentLike = c.ContentLike,
                //    ContentComments = c.Comments.Count,
                //    TopComments = c.Comments != null ? c.Comments.OrderByDescending(cc => cc.ContentID).Take(3).Select(m => new Business.DataTransferObjects.Mobile.V2.ContentCommentMobileModel { CommentNote = m.CommentNote, ProfileFullname = m.Profile.ProfileFullname, CommentID = m.CommentID }).ToList() : null,
                //    ContentURL = c.ContentURL,
                //    ContentID = c.ContentID,
                //    ContentTitle = c.ContentTitle,
                //    ContentDimension = c.ContentDimension,
                //    ContentTypeID = c.ContentType,
                //    ContentTypeExpression = c.ContentType1.ContentTypeExpression
                //}).ToList()
            }).ToList();

            //var tt = queryList.Select(w => new Business.DataTransferObjects.Mobile.V2.AlbumMobileModel
            //{                
            //    AlbumID = w.AlbumID,                
            //    AlbumDateExpire = w.AlbumDateExpire,
            //    AlbumDatePublish = w.AlbumDatePublish,
            //}).ToList();

            PaginationSet<Business.DataTransferObjects.Mobile.V2.AlbumMobileModel> result = new PaginationSet<Business.DataTransferObjects.Mobile.V2.AlbumMobileModel>()
            {
                Page = albumFilterMobileModel.Page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / albumFilterMobileModel.Size),
                Items = tt//queryList.Select(w => new Business.DataTransferObjects.Mobile.V2.AlbumMobileModel { ProfileID = w.ProfileID, AlbumID = w.AlbumID, AlbumCover = w.AlbumCover, AlbumDateCreated = w.AlbumDateCreated, AlbumName = w.AlbumName, AlbumAvailability = w.AlbumAvailability, AlbumDateExpire = w.AlbumDateExpire, AlbumDatePublish = w.AlbumDatePublish, AlbumAvailabilityName = w.AlbumType.AlbumTypeName, AlbumLike = w.Contents.Sum(m => m.ContentLike), AlbumContents = w.Contents.Count, AlbumComments = w.Contents.Sum(m => m.Comments.Count), AlbumHasComment = w.AlbumHasComment, Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c => new Business.DataTransferObjects.Mobile.V2.ContentMobileModel() { ContentLike = c.ContentLike, ContentURL = c.ContentURL, ContentID = c.ContentID, ContentTitle = c.ContentTitle, ContentComments = c.Comments.Count, ContentDimension = c.ContentDimension, ContentTypeExpression = c.ContentType1.ContentTypeExpression }).ToList() }).ToList()
            };

            return result;
        }

        public IQueryable<Album> getAlbumsByCategoryAction(Business.DataTransferObjects.Mobile.V2.AlbumFilterMobileModel albumFilterMobileModel)
        {
            IQueryable<Album> basequery;
            //Seperate based on Where ;)
            var currentDate = DateTime.UtcNow;
            switch (albumFilterMobileModel.CategoryID)
            {
                case 1:
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable(albumFilterMobileModel.CategoryID).OrderByDescending(m => m.AlbumView);
                    break;
                case 3:
                    //Not Expired
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.AlbumDateExpire >= currentDate && m.AlbumDatePublish <= currentDate).OrderByDescending(m => m.AlbumDatePublish);
                    break;
                case 12:
                    //Gorgias
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable(albumFilterMobileModel.CategoryID, albumFilterMobileModel.ProfileID).OrderByDescending(m => m.AlbumDateCreated);
                    break;
                case 15:
                    //Upcoming ;)
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.AlbumDatePublish >= currentDate).OrderBy(m => m.AlbumDatePublish);
                    break;
                case 16:
                    //Expiring Soon
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.AlbumDateExpire >= currentDate && m.AlbumDatePublish <= currentDate).OrderByDescending(m=> m.AlbumDateExpire).OrderByDescending(m => SqlFunctions.DateDiff("minute", m.AlbumDateExpire, m.AlbumDatePublish));//EntityFunctions.DiffMinutes(m.AlbumDateExpire,m.AlbumDatePublish)
                    break;
                case 17:
                    //Expired ;)
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.AlbumDateExpire <= currentDate).OrderByDescending(m => m.AlbumDateExpire);
                    break;
                case 21:
                    //iFelt ;)
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.ProfileActivities.Any(a => a.ProfileID == albumFilterMobileModel.ProfileID && a.ActivityType.ActivityTypeParentID == 2)).OrderByDescending(m => m.AlbumDatePublish);
                    break;
                case 22:
                    //stayOn ;)
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.Profile.Connections.Any(c => c.RequestedProfileID == albumFilterMobileModel.ProfileID && c.RequestTypeID == 3)).OrderByDescending(m => m.AlbumDatePublish);
                    break;
                case 23:
                    //myStories ;)
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.ProfileID == albumFilterMobileModel.ProfileID).OrderByDescending(m => m.AlbumDatePublish);
                    break;
                case 24:
                    //storyLand ;)
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.AlbumDatePublish <= currentDate).OrderByDescending(m => m.AlbumDatePublish);
                    break;
                case 25:
                    //BrandSo ;)
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.Profile.ProfileIsConfirmed == true && m.Profile.ProfileIsPeople == false && m.AlbumDatePublish < currentDate).OrderByDescending(m => m.AlbumDatePublish);
                    break;
                case 86:
                    //MicroApp Home All Stories ;)
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable().Where(m => m.ProfileID == albumFilterMobileModel.MicroAppProfileID).OrderByDescending(m => m.AlbumDatePublish);
                    break;
                default:
                    basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable(albumFilterMobileModel.CategoryID);
                    break;
            }

            //switch (albumFilterMobileModel.Page)
            //{
            //    case 1:
            //        basequery = basequery.OrderByDescending(m => m.AlbumView);
            //        break;
            //    case 2:
            //        basequery = basequery.OrderByDescending(m => m.AlbumID);
            //        break;
            //    default:
            //        break;
            //}



            return basequery;
        }

        public IQueryable<Album> getAlbumsByCategoryContent(Business.DataTransferObjects.Mobile.V2.AlbumFilterMobileModel albumFilterMobileModel)
        {
            IQueryable<Album> basequery;

            //switch (albumFilterMobileModel.Page)
            //{
            //    case 1:
            //        basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable(albumFilterMobileModel.CategoryID).OrderByDescending(m => m.AlbumView);
            //        break;
            //    case 2:
            //        basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable(albumFilterMobileModel.CategoryID).Where(wm => wm.Profile.ProfileIsPeople == true && wm.Profile.ProfileStatus == true).OrderByDescending(m => m.AlbumView);
            //        break;
            //    default:
            //        var currentDate = DateTime.UtcNow;
            //        basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable(albumFilterMobileModel.CategoryID).Where(wm=> wm.AlbumDatePublish <= currentDate).OrderByDescending(m => m.AlbumDatePublish);
            //        break;
            //}

            var currentDate = DateTime.UtcNow;
            if (albumFilterMobileModel.isMicroApp == false)
            {
                basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryAsQueryable(albumFilterMobileModel.CategoryID).Where(wm => wm.AlbumDatePublish <= currentDate).OrderByDescending(m => m.AlbumDatePublish);
            }
            else
            {
                basequery = DataLayer.DataLayerFacade.AlbumRepository().GetV2AlbumByCategoryForMicroAppAsQueryable(albumFilterMobileModel.CategoryID).Where(wm => wm.AlbumDatePublish <= currentDate && wm.ProfileID == albumFilterMobileModel.MicroAppProfileID).OrderByDescending(m => m.AlbumDatePublish);
            }

            return basequery;
        }

        public Business.DataTransferObjects.Mobile.V2.AlbumMobileModel getAlbum(int AlbumID, int ProfileID, int DeviceWidth)
        {
            Album w = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumV2Mobile(AlbumID, ProfileID);

            //Add ProfileID as Viewer in ProfileActivity ;)

            new ProfileActivityFacade().Insert(new Business.DataTransferObjects.Mobile.V2.ProfileActivityUpdateMobileModel { ActivityTypeID = 14, AlbumID = AlbumID, ProfileActivityCount = 1, ProfileActivityIsFirst = true, ProfileID = ProfileID, Share = "", Location = "" });

            Business.DataTransferObjects.Mobile.V2.AlbumMobileModel result = new Business.DataTransferObjects.Mobile.V2.AlbumMobileModel
            {
                ProfileID = w.ProfileID,
                AlbumID = w.AlbumID,
                AlbumCover = w.AlbumCover,
                AlbumDateCreated = w.AlbumDateCreated,
                AlbumName = w.AlbumName,
                AlbumAvailability = w.AlbumAvailability,
                AlbumDateExpire = w.AlbumDateExpire,
                AlbumDatePublish = w.AlbumDatePublish,
                CategoryName = w.Category.CategoryName,
                CategoryID = w.CategoryID,
                //AlbumAvailabilityName = w.AlbumType.AlbumTypeName,
                AlbumLike = w.Contents.Sum(m => m.ContentLike),
                AlbumContents = w.Contents.Count,
                AlbumComments = w.Contents.Sum(m => m.Comments.Count),
                AlbumHasComment = w.AlbumHasComment,
                AlbumView = w.AlbumView,
                AlbumRepostRequest = w.AlbumRepostRequest,
                AlbumRepostValue = w.AlbumRepostValue,
                AlbumParentID = w.AlbumParentID,
                AlbumParentProfileID = w.AlbumParent != null ? w.AlbumParent.ProfileID : 0,
                AlbumRelatedInfo = w.AlbumParent != null ? new Business.DataTransferObjects.Mobile.V2.AlbumProfileRelatedMobileModel
                {
                    ProfileID = w.AlbumParent.ProfileID,
                    AlbumID = w.AlbumParentID.Value,
                    ProfileFullname = w.AlbumParent.Profile.ProfileFullname,
                    AlbumCover = w.AlbumParent.AlbumCover,
                    AlbumPublishDate = w.AlbumDatePublish,
                    AlbumTitle = w.AlbumParent.AlbumName,
                    DeviceWidth = DeviceWidth,
                    ContentDimension = w.AlbumParent.Contents.OrderBy(m => m.ContentID).FirstOrDefault().ContentDimension
                } : null,
                isFelt = w.ProfileActivities.Any(m => m.ProfileID == ProfileID),
                isSubscribed = w.Profile.Connections.Any(m => m.RequestedProfileID == ProfileID),
                Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c => new Business.DataTransferObjects.Mobile.V2.ContentMobileModel()
                {
                    ContentLike = c.ContentLike,
                    ContentComments = c.Comments.Count,
                    TopComments = c.Comments.OrderByDescending(cc => cc.ContentID).Take(3).Select(m => new Business.DataTransferObjects.Mobile.V2.ContentCommentMobileModel { CommentNote = m.CommentNote, ProfileFullname = m.Profile.ProfileFullname, CommentID = m.CommentID }).ToList(),
                    ContentURL = c.ContentURL,
                    ContentID = c.ContentID,
                    ContentTitle = c.ContentTitle,
                    ContentDimension = c.ContentDimension,
                    ContentTypeID = c.ContentType,
                    ContentTypeExpression = c.ContentType1.ContentTypeExpression,
                    ContentTypeName = c.ContentType1.ContentTypeName,
                    DeviceWidth = DeviceWidth,
                }).ToList()
            };

            return result;
        }

        public Business.DataTransferObjects.Mobile.V2.AlbumMobileModel getAlbum(int AlbumID, int ProfileID, int DeviceWidth, Business.DataTransferObjects.Mobile.V2.ProfileActivityUpdateMobileModel Activity)
        {
            Album w = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumV2Mobile(AlbumID, ProfileID);

            //Add ProfileID as Viewer in ProfileActivity ;)

            new ProfileActivityFacade().Insert(new Business.DataTransferObjects.Mobile.V2.ProfileActivityUpdateMobileModel { ActivityTypeID = 14, AlbumID = AlbumID, ProfileActivityCount = 1, ProfileActivityIsFirst = Activity.ProfileActivityIsFirst, ProfileID = ProfileID, Share = Activity.Share, Location = "" });

            Business.DataTransferObjects.Mobile.V2.AlbumMobileModel result = new Business.DataTransferObjects.Mobile.V2.AlbumMobileModel
            {
                ProfileID = w.ProfileID,
                AlbumID = w.AlbumID,
                AlbumCover = w.AlbumCover,
                AlbumDateCreated = w.AlbumDateCreated,
                AlbumName = w.AlbumName,
                AlbumAvailability = w.AlbumAvailability,
                AlbumDateExpire = w.AlbumDateExpire,
                AlbumDatePublish = w.AlbumDatePublish,
                CategoryName = w.Category.CategoryName,
                CategoryID = w.CategoryID,
                //AlbumAvailabilityName = w.AlbumType.AlbumTypeName,
                AlbumLike = w.Contents.Sum(m => m.ContentLike),
                AlbumContents = w.Contents.Count,
                AlbumComments = w.Contents.Sum(m => m.Comments.Count),
                AlbumHasComment = w.AlbumHasComment,
                AlbumView = w.AlbumView,
                AlbumRepostRequest = w.AlbumRepostRequest,
                AlbumRepostValue = w.AlbumRepostValue,
                AlbumParentID = w.AlbumParentID,
                AlbumParentProfileID = w.AlbumParent != null ? w.AlbumParent.ProfileID : 0,
                AlbumRelatedInfo = w.AlbumParent != null ? new Business.DataTransferObjects.Mobile.V2.AlbumProfileRelatedMobileModel
                {
                    ProfileID = w.AlbumParent.ProfileID,
                    AlbumID = w.AlbumParentID.Value,
                    ProfileFullname = w.AlbumParent.Profile.ProfileFullname,
                    AlbumCover = w.AlbumParent.AlbumCover,
                    AlbumPublishDate = w.AlbumDatePublish,
                    AlbumTitle = w.AlbumParent.AlbumName,
                    DeviceWidth = DeviceWidth,
                    ContentDimension = w.AlbumParent.Contents.OrderBy(m => m.ContentID).FirstOrDefault().ContentDimension
                } : null,
                isFelt = w.ProfileActivities.Any(m => m.ProfileID == ProfileID),
                isSubscribed = w.Profile.Connections.Any(m => m.RequestedProfileID == ProfileID),
                Contents = w.Contents.OrderByDescending(c => c.ContentCreatedDate).Select(c => new Business.DataTransferObjects.Mobile.V2.ContentMobileModel()
                {
                    ContentLike = c.ContentLike,
                    ContentComments = c.Comments.Count,
                    TopComments = c.Comments.OrderByDescending(cc => cc.ContentID).Take(3).Select(m => new Business.DataTransferObjects.Mobile.V2.ContentCommentMobileModel { CommentNote = m.CommentNote, ProfileFullname = m.Profile.ProfileFullname, CommentID = m.CommentID }).ToList(),
                    ContentURL = c.ContentURL,
                    ContentID = c.ContentID,
                    ContentTitle = c.ContentTitle,
                    ContentDimension = c.ContentDimension,
                    ContentTypeID = c.ContentType,
                    ContentTypeExpression = c.ContentType1.ContentTypeExpression,
                    ContentTypeName = c.ContentType1.ContentTypeName,
                    DeviceWidth = DeviceWidth,
                }).ToList()
            };

            return result;
        }

        public Business.DataTransferObjects.Mobile.V2.AlbumUpdateMobileModel GetAlbumV2(int AlbumID)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().GetAlbumV2(AlbumID);
        }

        //V2 End

        public AlbumDTO GetAlbum(int AlbumID)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().GetAlbum(AlbumID));
            return result;
        }

        public bool UpdateAlbumComment(int AlbumID)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().UpdateComment(AlbumID);
            return result;
        }

        public bool UpdateAlbumView(int AlbumID)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().Update(AlbumID);
            return result;
        }

        public DTResult<AlbumDTO> FilterResult(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable();

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.Profile.ProfileFullname.ToLower().Contains(search.ToLower()) || p.AlbumName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Album>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AlbumDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AlbumDTO> result = new DTResult<AlbumDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<AlbumDTO> GetAlbums(int page, int pagesize)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable();

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Album>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AlbumDTO> result = new PaginationSet<AlbumDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AlbumDTO>>(queryList.ToList())
            };

            return result;
        }

        public List<AlbumDTO> GetAlbums()
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable().Take(200);
            return Mapper.Map<List<AlbumDTO>>(basequery.ToList());
        }

        public List<AlbumContentDTO> GetAlbumsForContents()
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllForInsertContentAsQueryable().Take(1000);
            return Mapper.Map<List<AlbumContentDTO>>(basequery.ToList());
        }


        public DTResult<AlbumDTO> FilterResultByCategoryID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int CategoryID)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable().Where(m => m.CategoryID == CategoryID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.AlbumName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Album>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AlbumDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AlbumDTO> result = new DTResult<AlbumDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<AlbumDTO> GetAlbumsByCategoryID(int CategoryID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsByCategoryIDAsQueryable(CategoryID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Album>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AlbumDTO> result = new PaginationSet<AlbumDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AlbumDTO>>(queryList.ToList())
            };

            return result;
        }

        public DTResult<AlbumDTO> FilterResultByProfileID(string search, string sortOrder, int start, int length, List<string> columnFilters, DTParameters param, int ProfileID)
        {
            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsAllAsQueryable().Where(m => m.ProfileID == ProfileID);

            if (search.Length > 0)
            {
                basequery = basequery.Where(p => (p.AlbumName.ToLower().Contains(search.ToLower())));
            }

            var queryList = RepositoryHelper.PaginationDatatables<Album>(start, length, basequery).Future();
            var queryTotal = basequery.FutureCount();

            var resultList = Mapper.Map<List<AlbumDTO>>(queryList.ToList());
            int intTotal = queryTotal.Value;

            DTResult<AlbumDTO> result = new DTResult<AlbumDTO>
            {
                draw = param.Draw,
                recordsFiltered = intTotal,
                recordsTotal = intTotal,
                data = resultList
            };
            return result;
        }

        public PaginationSet<AlbumDTO> GetAlbumsByProfileID(int ProfileID, int page, int pagesize)
        {

            var basequery = DataLayer.DataLayerFacade.AlbumRepository().GetAlbumsByProfileIDAsQueryable(ProfileID);

            var queryList = DataLayer.Repository.RepositoryHelper.Pagination<Album>(page, pagesize, basequery).Future();
            var queryTotal = basequery.FutureCount();

            int intTotal = queryTotal.Value;

            PaginationSet<AlbumDTO> result = new PaginationSet<AlbumDTO>()
            {
                Page = page,
                TotalCount = intTotal,
                TotalPages = (int)Math.Ceiling((decimal)intTotal / pagesize),
                Items = Mapper.Map<List<AlbumDTO>>(queryList.ToList())
            };

            return result;
        }

        public AlbumDTO Insert(AlbumDTO objAlbum)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().Insert(objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new AlbumDTO();
            }
        }

        public AlbumDTO InsertForMobile(AlbumDTO objAlbum)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().Insert(objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumDatePublish, objAlbum.AlbumAvailability, true, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID, objAlbum.AlbumHasComment));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new AlbumDTO();
            }
        }

        public AlbumDTO InsertHottest(AlbumDTO objAlbum)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().Insert(objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumDatePublish, objAlbum.AlbumAvailability, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID, objAlbum.AlbumHasComment));

            if (result != null)
            {
                ContentDTO obj = new ContentDTO { AlbumID = result.AlbumID, ContentCreatedDate = DateTime.UtcNow, ContentIsDeleted = false, ContentStatus = false, ContentTitle = result.AlbumName, ContentType = 1, ContentURL = result.AlbumCover };
                Facade.ContentFacade().Insert(obj);
                return result;
            }
            else
            {
                return new AlbumDTO();
            }
        }

        public bool Delete(int AlbumID)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().Delete(AlbumID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateHottest(int AlbumID, AlbumDTO objAlbum)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().Update(objAlbum.AlbumID, objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumDatePublish, objAlbum.AlbumAvailability, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID, objAlbum.AlbumHasComment);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(int AlbumID, AlbumDTO objAlbum)
        {
            bool result = DataLayer.DataLayerFacade.AlbumRepository().Update(objAlbum.AlbumID, objAlbum.AlbumName, objAlbum.AlbumDateCreated, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.AlbumIsDeleted, objAlbum.CategoryID, objAlbum.ProfileID);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //V2 fx ;)
        public AlbumDTO InsertV2(Business.DataTransferObjects.Mobile.V2.AlbumUpdateMobileModel objAlbum)
        {
            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().Insert(objAlbum.AlbumName, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.CategoryID, objAlbum.ProfileID, objAlbum.AlbumDatePublish, objAlbum.AlbumAvailability, objAlbum.AlbumHasComment, objAlbum.AlbumReadingLanguageCode, objAlbum.AlbumRepostValue, objAlbum.AlbumRepostRequest, objAlbum.AlbumRepostAttempt, objAlbum.AlbumPrice, objAlbum.AlbumIsTokenAvailable, objAlbum.AlbumPriceToken, objAlbum.ContentRatingID, objAlbum.Contents, objAlbum.AlbumParentID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new AlbumDTO();
            }
        }

        public AlbumDTO InsertV2Topic(Business.DataTransferObjects.Mobile.V2.AlbumUpdateV2MobileModel objAlbum)
        {
            if (!objAlbum.AlbumDatePublish.HasValue)
            {
                objAlbum.AlbumDatePublish = DateTime.UtcNow;
            }

            AlbumDTO result = Mapper.Map<AlbumDTO>(DataLayer.DataLayerFacade.AlbumRepository().Insert(objAlbum.AlbumName, objAlbum.AlbumStatus, objAlbum.AlbumCover, objAlbum.CategoryID, objAlbum.ProfileID, objAlbum.AlbumDatePublish.Value, objAlbum.AlbumAvailability, objAlbum.AlbumHasComment, objAlbum.AlbumReadingLanguageCode, objAlbum.AlbumRepostValue, objAlbum.AlbumRepostRequest, objAlbum.AlbumRepostAttempt, objAlbum.AlbumPrice, objAlbum.AlbumIsTokenAvailable, objAlbum.AlbumPriceToken, objAlbum.ContentRatingID, objAlbum.Contents, objAlbum.Topic, objAlbum.AlbumParentID));

            if (result != null)
            {
                return result;
            }
            else
            {
                return new AlbumDTO();
            }
        }

        public bool PublishAlbum(int AlbumID)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().UpdatePublishAlbum(AlbumID);
        }

        public bool RepostAlbum(int AlbumID)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().UpdateRepostAlbum(AlbumID);
        }

        public bool RequestRepostAlbum(int AlbumID)
        {
            return DataLayer.DataLayerFacade.AlbumRepository().UpdateRequestRepostAlbum(AlbumID);
        }

    }
}