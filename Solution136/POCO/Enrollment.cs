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
    
    public partial class enrollment
    {
        public string student_id { get; set; }
        public int schedule_id { get; set; }
        public string grade { get; set; }
    
        public virtual course_schedule course_schedule { get; set; }
        public virtual student student { get; set; }
    }
}
