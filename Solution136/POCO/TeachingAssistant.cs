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
    
    public partial class TeachingAssistant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeachingAssistant()
        {
            this.course_schedule = new HashSet<course_schedule>();
        }
    
        public int ta_id { get; set; }
        public int ta_type_id { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    
        public virtual TeachingAssistantType TeachingAssistantType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<course_schedule> course_schedule { get; set; }
    }
}