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
    
    public partial class ContentRating
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContentRating()
        {
            this.Albums = new HashSet<Album>();
            this.ContentRating1 = new HashSet<ContentRating>();
        }
    
        public int ContentRatingID { get; set; }
        public string ContentRatingName { get; set; }
        public int ContentRatingAge { get; set; }
        public bool ContentRatingStatus { get; set; }
        public string ContentRatingImage { get; set; }
        public string ContentRatingDescription { get; set; }
        public int ContentRatingOrder { get; set; }
        public string ContentRatingLanguageCode { get; set; }
        public Nullable<int> ContentRatingParentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Albums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContentRating> ContentRating1 { get; set; }
        public virtual ContentRating ContentRating2 { get; set; }
    }
}
