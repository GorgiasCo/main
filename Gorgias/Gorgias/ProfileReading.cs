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
    
    public partial class ProfileReading
    {
        public int ProfileReadingID { get; set; }
        public string ProfileReadingLanguageCode { get; set; }
        public System.DateTime ProfileReadingDatetime { get; set; }
        public int ProfileID { get; set; }
    
        public virtual Profile Profile { get; set; }
    }
}
