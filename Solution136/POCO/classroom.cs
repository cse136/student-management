//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POCO
{
    using System;
    using System.Collections.Generic;
    
    public partial class classroom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public classroom()
        {
            this.course_schedule = new HashSet<course_schedule>();
        }
    
        public int classroom_id { get; set; }
        public string classroom_name { get; set; }
        public int building_id { get; set; }
        public int max_students { get; set; }
    
        public virtual building building { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<course_schedule> course_schedule { get; set; }
    }
}
