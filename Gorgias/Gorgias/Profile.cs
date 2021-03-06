//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Profile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profile()
        {
            this.Addresses = new HashSet<Address>();
            this.Albums = new HashSet<Album>();
            this.Connections = new HashSet<Connection>();
            this.Connections1 = new HashSet<Connection>();
            this.ExternalLinks = new HashSet<ExternalLink>();
            this.FeaturedSponsors = new HashSet<FeaturedSponsor>();
            this.Messages = new HashSet<Message>();
            this.ProfileAttributes = new HashSet<ProfileAttribute>();
            this.ProfileSocialNetworks = new HashSet<ProfileSocialNetwork>();
            this.ProfileTags = new HashSet<ProfileTag>();
            this.UserProfiles = new HashSet<UserProfile>();
            this.Messages1 = new HashSet<Message>();
            this.Industries = new HashSet<Industry>();
            this.Comments = new HashSet<Comment>();
            this.ProfileCommissions = new HashSet<ProfileCommission>();
            this.ProfileReports = new HashSet<ProfileReport>();
            this.ProfileReadings = new HashSet<ProfileReading>();
            this.ProfileTokens = new HashSet<ProfileToken>();
            this.ProfileActivities = new HashSet<ProfileActivity>();
            this.Categories = new HashSet<Category>();
        }
    
        public int ProfileID { get; set; }
        public string ProfileFullname { get; set; }
        public bool ProfileIsPeople { get; set; }
        public bool ProfileIsDeleted { get; set; }
        public System.DateTime ProfileDateCreated { get; set; }
        public string ProfileDescription { get; set; }
        public long ProfileView { get; set; }
        public int ProfileLike { get; set; }
        public string ProfileURL { get; set; }
        public string ProfileShortDescription { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileEmail { get; set; }
        public bool ProfileStatus { get; set; }
        public bool ProfileIsConfirmed { get; set; }
        public int ProfileTypeID { get; set; }
        public int ThemeID { get; set; }
        public int SubscriptionTypeID { get; set; }
        public int ProfileCredit { get; set; }
        public string ProfileFullnameEnglish { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Address> Addresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Albums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connection> Connections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connection> Connections1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExternalLink> ExternalLinks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeaturedSponsor> FeaturedSponsors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ProfileType ProfileType { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
        public virtual Theme Theme { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileAttribute> ProfileAttributes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileSocialNetwork> ProfileSocialNetworks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileTag> ProfileTags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Industry> Industries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileCommission> ProfileCommissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileReport> ProfileReports { get; set; }
        public virtual ProfileSetting ProfileSetting { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileReading> ProfileReadings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileToken> ProfileTokens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileActivity> ProfileActivities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }
    }
}
