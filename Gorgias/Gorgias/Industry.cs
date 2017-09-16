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
    
    public partial class Industry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Industry()
        {
            this.Profiles = new HashSet<Profile>();
            this.IndustryChilds = new HashSet<Industry>();
        }
    
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }
        public bool IndustryStatus { get; set; }
        public Nullable<int> IndustryParentID { get; set; }
        public string IndustryImage { get; set; }
        public string IndustryDescription { get; set; }
        public string IndustryLanguageCode { get; set; }
        public Nullable<int> IndustryOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Profile> Profiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Industry> IndustryChilds { get; set; }
        public virtual Industry IndustryParent { get; set; }
    }
}
