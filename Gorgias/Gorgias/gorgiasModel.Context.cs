﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gorgias
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GorgiasEntities : DbContext
    {
        public GorgiasEntities()
            : base("name=GorgiasEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<ExternalLink> ExternalLinks { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<FeaturedSponsor> FeaturedSponsors { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<LinkType> LinkTypes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<ProfileAttribute> ProfileAttributes { get; set; }
        public virtual DbSet<ProfileSocialNetwork> ProfileSocialNetworks { get; set; }
        public virtual DbSet<ProfileTag> ProfileTags { get; set; }
        public virtual DbSet<ProfileType> ProfileTypes { get; set; }
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<SocialNetwork> SocialNetworks { get; set; }
        public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<AlbumType> AlbumTypes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<ProfileCommission> ProfileCommissions { get; set; }
        public virtual DbSet<ProfileReport> ProfileReports { get; set; }
        public virtual DbSet<ReportType> ReportTypes { get; set; }
        public virtual DbSet<Revenue> Revenues { get; set; }
    
        public virtual int updateProfileLike(Nullable<int> profileID)
        {
            var profileIDParameter = profileID.HasValue ?
                new ObjectParameter("ProfileID", profileID) :
                new ObjectParameter("ProfileID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateProfileLike", profileIDParameter);
        }
    }
}
