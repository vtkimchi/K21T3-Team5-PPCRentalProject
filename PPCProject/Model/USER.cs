//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPCProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class USER
    {
        public USER()
        {
            this.PROPERTies = new HashSet<PROPERTY>();
            this.PROPERTies1 = new HashSet<PROPERTY>();
        }
    
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual ICollection<PROPERTY> PROPERTies { get; set; }
        public virtual ICollection<PROPERTY> PROPERTies1 { get; set; }
    }
}
