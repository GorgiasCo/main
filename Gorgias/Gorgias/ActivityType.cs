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
    
    public partial class ActivityType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActivityType()
        {
            this.ActivityTypeChilds = new HashSet<ActivityType>();
            this.ProfileActivities = new HashSet<ProfileActivity>();
        }
    
        public int ActivityTypeID { get; set; }
        public string ActivityTypeName { get; set; }
        public string ActivityTypeLanguageCode { get; set; }
        public bool ActivityTypeStatus { get; set; }
        public Nullable<int> ActivityTypeParentID { get; set; }
        public int ActivityTypeOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityType> ActivityTypeChilds { get; set; }
        public virtual ActivityType ActivityTypeParent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileActivity> ProfileActivities { get; set; }
    }
}
