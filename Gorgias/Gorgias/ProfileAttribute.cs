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
    
    public partial class ProfileAttribute
    {
        public int AttributeID { get; set; }
        public int ProfileID { get; set; }
        public string ProfileAttributeNote { get; set; }
    
        public virtual Attribute Attribute { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
