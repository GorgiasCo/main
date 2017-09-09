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
    
    public partial class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            this.Cities = new HashSet<City>();
            this.Users = new HashSet<User>();
            this.CountryChilds = new HashSet<Country>();
        }
    
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryShortName { get; set; }
        public Nullable<bool> CountryStatus { get; set; }
        public string CountryPhoneCode { get; set; }
        public string CountryImage { get; set; }
        public string CountryDescription { get; set; }
        public string CountryLanguageCode { get; set; }
        public Nullable<int> CountryParentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<City> Cities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Country> CountryChilds { get; set; }
        public virtual Country CountryParent { get; set; }
    }
}
