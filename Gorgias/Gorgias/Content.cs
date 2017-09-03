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
    
    public partial class Content
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Content()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int ContentID { get; set; }
        public string ContentTitle { get; set; }
        public string ContentURL { get; set; }
        public int ContentType { get; set; }
        public bool ContentStatus { get; set; }
        public bool ContentIsDeleted { get; set; }
        public int AlbumID { get; set; }
        public Nullable<System.DateTime> ContentCreatedDate { get; set; }
        public int ContentLike { get; set; }
        public System.Data.Entity.Spatial.DbGeography ContentGeoLocation { get; set; }
        public string ContentDimension { get; set; }
    
        public virtual Album Album { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ContentType ContentType1 { get; set; }
    }
}
