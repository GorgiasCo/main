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
    
    public partial class Address
    {
        public int AddressID { get; set; }
        public string AddressName { get; set; }
        public bool AddressStatus { get; set; }
        public string AddressTel { get; set; }
        public string AddressFax { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressAddress { get; set; }
        public string AddressEmail { get; set; }
        public string AddressImage { get; set; }
        public int CityID { get; set; }
        public int ProfileID { get; set; }
        public int AddressTypeID { get; set; }
        public System.Data.Entity.Spatial.DbGeography AddressLocation { get; set; }
    
        public virtual AddressType AddressType { get; set; }
        public virtual City City { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
