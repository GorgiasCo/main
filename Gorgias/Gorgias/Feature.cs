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
    
    public partial class Feature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Feature()
        {
            this.FeaturedSponsors = new HashSet<FeaturedSponsor>();
        }
    
        public int FeatureID { get; set; }
        public string FeatureTitle { get; set; }
        public System.DateTime FeatureDateCreated { get; set; }
        public System.DateTime FeatureDateExpired { get; set; }
        public bool FeatureStatus { get; set; }
        public bool FeatureIsDeleted { get; set; }
        public string FeatureImage { get; set; }
        public string FeatureDescription { get; set; }
        public int ProfileID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeaturedSponsor> FeaturedSponsors { get; set; }
    }
}
