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
    
    public partial class ProfileCommission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProfileCommission()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public int ProfileCommissionID { get; set; }
        public double ProfileCommissionRate { get; set; }
        public System.DateTime ProfileCommissionDateCreated { get; set; }
        public bool ProfileCommissionStatus { get; set; }
        public int ProfileID { get; set; }
        public int UserID { get; set; }
        public int UserRoleID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}