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
    
    public partial class SubscriptionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubscriptionType()
        {
            this.Profiles = new HashSet<Profile>();
        }
    
        public int SubscriptionTypeID { get; set; }
        public string SubscriptionTypeName { get; set; }
        public decimal SubscriptionTypeFee { get; set; }
        public bool SubscriptionTypeStatus { get; set; }
        public string SubscriptionTypeImage { get; set; }
        public string SubscriptionTypeDescription { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}