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
    
    public partial class UserProfile
    {
        public int ProfileID { get; set; }
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
    
        public virtual Profile Profile { get; set; }
        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
