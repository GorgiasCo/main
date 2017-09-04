using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using Gorgias.Business.DataTransferObjects;
namespace Gorgias
{       	     
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>{cfg.CreateMap<Address, Business.DataTransferObjects.AddressDTO>();
                cfg.CreateMap<Address, Business.DataTransferObjects.Web.AddressModelV2>();
                cfg.CreateMap<AddressType, Business.DataTransferObjects.AddressTypeDTO>();
                cfg.CreateMap<Album, Business.DataTransferObjects.AlbumDTO>();
                cfg.CreateMap<Attribute, Business.DataTransferObjects.AttributeDTO>();
                cfg.CreateMap<Category, Business.DataTransferObjects.CategoryDTO>();
                cfg.CreateMap<City, Business.DataTransferObjects.CityDTO>();
                cfg.CreateMap<Connection, Business.DataTransferObjects.ConnectionDTO>();
                cfg.CreateMap<Content, Business.DataTransferObjects.ContentDTO>();
                cfg.CreateMap<Country, Business.DataTransferObjects.CountryDTO>();
                cfg.CreateMap<ExternalLink, Business.DataTransferObjects.ExternalLinkDTO>();
                cfg.CreateMap<Feature, Business.DataTransferObjects.FeatureDTO>();
                cfg.CreateMap<FeaturedSponsor, Business.DataTransferObjects.FeaturedSponsorDTO>();
                cfg.CreateMap<Industry, Business.DataTransferObjects.IndustryDTO>();
                cfg.CreateMap<LinkType, Business.DataTransferObjects.LinkTypeDTO>();
                cfg.CreateMap<Message, Business.DataTransferObjects.MessageDTO>();
                cfg.CreateMap<Profile, Business.DataTransferObjects.ProfileDTO>();
                cfg.CreateMap<ProfileSocialNetwork, Business.DataTransferObjects.ProfileSocialNetworkDTO>();
                cfg.CreateMap<ProfileTag, Business.DataTransferObjects.ProfileTagDTO>();
                cfg.CreateMap<ProfileType, Business.DataTransferObjects.ProfileTypeDTO>();
                cfg.CreateMap<RequestType, Business.DataTransferObjects.RequestTypeDTO>();
                cfg.CreateMap<SocialNetwork, Business.DataTransferObjects.SocialNetworkDTO>();
                cfg.CreateMap<SubscriptionType, Business.DataTransferObjects.SubscriptionTypeDTO>();
                cfg.CreateMap<Tag, Business.DataTransferObjects.TagDTO>();
                cfg.CreateMap<Theme, Business.DataTransferObjects.ThemeDTO>();
                cfg.CreateMap<User, Business.DataTransferObjects.UserDTO>();
                cfg.CreateMap<UserProfile, Business.DataTransferObjects.UserProfileDTO>();
                cfg.CreateMap<UserRole, Business.DataTransferObjects.UserRoleDTO>();
                cfg.CreateMap<ProfileAttribute, Business.DataTransferObjects.ProfileAttributeDTO>();
                cfg.CreateMap<Newsletter, Business.DataTransferObjects.NewsletterDTO>();
                cfg.CreateMap<AlbumType, Business.DataTransferObjects.AlbumTypeDTO>();
                cfg.CreateMap<User, UserCustomDTO>();
                cfg.CreateMap<Comment, Business.DataTransferObjects.CommentDTO>();
                cfg.CreateMap<ReportType, Business.DataTransferObjects.ReportTypeDTO>();
                cfg.CreateMap<Revenue, Business.DataTransferObjects.RevenueDTO>();
                cfg.CreateMap<ProfileReport, Business.DataTransferObjects.ProfileReportDTO>();
                cfg.CreateMap<ProfileCommission, Business.DataTransferObjects.ProfileCommissionDTO>();
                cfg.CreateMap<Payment, Business.DataTransferObjects.PaymentDTO>();
                cfg.CreateMap<FBActivity, Business.DataTransferObjects.FBActivityDTO>();
                cfg.CreateMap<ActivityType, Business.DataTransferObjects.ActivityTypeDTO>();
                cfg.CreateMap<ContentRating, Business.DataTransferObjects.ContentRatingDTO>();
                cfg.CreateMap<ContentType, Business.DataTransferObjects.ContentTypeDTO>();
                cfg.CreateMap<Language, Business.DataTransferObjects.LanguageDTO>();
                cfg.CreateMap<Quote, Business.DataTransferObjects.QuoteDTO>();
                cfg.CreateMap<ProfileToken, Business.DataTransferObjects.ProfileTokenDTO>();
                cfg.CreateMap<ProfileReading, Business.DataTransferObjects.ProfileReadingDTO>();
                cfg.CreateMap<ProfileSetting, Business.DataTransferObjects.ProfileSettingDTO>();
            });   
        }
    }
}   

