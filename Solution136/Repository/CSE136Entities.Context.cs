﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class cse136Entities : DbContext
    {
        public cse136Entities()
            : base("name=cse136Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<building> buildings { get; set; }
        public virtual DbSet<classroom> classrooms { get; set; }
        public virtual DbSet<course> courses { get; set; }
        public virtual DbSet<course_review> course_review { get; set; }
        public virtual DbSet<course_schedule> course_schedule { get; set; }
        public virtual DbSet<enrollment> enrollments { get; set; }
        public virtual DbSet<instructor> instructors { get; set; }
        public virtual DbSet<schedule_day> schedule_day { get; set; }
        public virtual DbSet<schedule_time> schedule_time { get; set; }
        public virtual DbSet<schedule_tutor> schedule_tutor { get; set; }
        public virtual DbSet<staff> staffs { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TeachingAssistant> TeachingAssistants { get; set; }
        public virtual DbSet<TeachingAssistantType> TeachingAssistantTypes { get; set; }
    
        public virtual int dba_insert_initial_data()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("dba_insert_initial_data");
        }
    
        public virtual ObjectResult<ic_136_test_get_ta_info_Result> ic_136_test_get_ta_info(Nullable<int> ta_id)
        {
            var ta_idParameter = ta_id.HasValue ?
                new ObjectParameter("ta_id", ta_id) :
                new ObjectParameter("ta_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ic_136_test_get_ta_info_Result>("ic_136_test_get_ta_info", ta_idParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int spDeleteStudentInfo(string student_id)
        {
            var student_idParameter = student_id != null ?
                new ObjectParameter("student_id", student_id) :
                new ObjectParameter("student_id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeleteStudentInfo", student_idParameter);
        }
    
        public virtual int spDeleteStudentSchedule(string student_id, Nullable<int> schedule_id)
        {
            var student_idParameter = student_id != null ?
                new ObjectParameter("student_id", student_id) :
                new ObjectParameter("student_id", typeof(string));
    
            var schedule_idParameter = schedule_id.HasValue ?
                new ObjectParameter("schedule_id", schedule_id) :
                new ObjectParameter("schedule_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeleteStudentSchedule", student_idParameter, schedule_idParameter);
        }
    
        public virtual ObjectResult<spGetCourseList_Result> spGetCourseList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetCourseList_Result>("spGetCourseList");
        }
    
        public virtual ObjectResult<spGetLoginInfo_Result> spGetLoginInfo(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetLoginInfo_Result>("spGetLoginInfo", emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<spGetScheduleList_Result> spGetScheduleList(Nullable<int> year, string quarter)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            var quarterParameter = quarter != null ?
                new ObjectParameter("quarter", quarter) :
                new ObjectParameter("quarter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetScheduleList_Result>("spGetScheduleList", yearParameter, quarterParameter);
        }
    
        public virtual ObjectResult<spGetStudentInfo_Result> spGetStudentInfo(string student_id)
        {
            var student_idParameter = student_id != null ?
                new ObjectParameter("student_id", student_id) :
                new ObjectParameter("student_id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetStudentInfo_Result>("spGetStudentInfo", student_idParameter);
        }
    
        public virtual ObjectResult<spGetStudentList_Result> spGetStudentList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetStudentList_Result>("spGetStudentList");
        }
    
        public virtual int spInsertStudentInfo(string student_id, string ssn, string first_name, string last_name, string email, string password, Nullable<double> shoe_size, Nullable<int> weight)
        {
            var student_idParameter = student_id != null ?
                new ObjectParameter("student_id", student_id) :
                new ObjectParameter("student_id", typeof(string));
    
            var ssnParameter = ssn != null ?
                new ObjectParameter("ssn", ssn) :
                new ObjectParameter("ssn", typeof(string));
    
            var first_nameParameter = first_name != null ?
                new ObjectParameter("first_name", first_name) :
                new ObjectParameter("first_name", typeof(string));
    
            var last_nameParameter = last_name != null ?
                new ObjectParameter("last_name", last_name) :
                new ObjectParameter("last_name", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var shoe_sizeParameter = shoe_size.HasValue ?
                new ObjectParameter("shoe_size", shoe_size) :
                new ObjectParameter("shoe_size", typeof(double));
    
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spInsertStudentInfo", student_idParameter, ssnParameter, first_nameParameter, last_nameParameter, emailParameter, passwordParameter, shoe_sizeParameter, weightParameter);
        }
    
        public virtual int spInsertStudentSchedule(string student_id, Nullable<int> schedule_id)
        {
            var student_idParameter = student_id != null ?
                new ObjectParameter("student_id", student_id) :
                new ObjectParameter("student_id", typeof(string));
    
            var schedule_idParameter = schedule_id.HasValue ?
                new ObjectParameter("schedule_id", schedule_id) :
                new ObjectParameter("schedule_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spInsertStudentSchedule", student_idParameter, schedule_idParameter);
        }
    
        public virtual int spUpdateStudentInfo(string student_id, string ssn, string first_name, string last_name, string email, string password, Nullable<double> shoe_size, Nullable<int> weight)
        {
            var student_idParameter = student_id != null ?
                new ObjectParameter("student_id", student_id) :
                new ObjectParameter("student_id", typeof(string));
    
            var ssnParameter = ssn != null ?
                new ObjectParameter("ssn", ssn) :
                new ObjectParameter("ssn", typeof(string));
    
            var first_nameParameter = first_name != null ?
                new ObjectParameter("first_name", first_name) :
                new ObjectParameter("first_name", typeof(string));
    
            var last_nameParameter = last_name != null ?
                new ObjectParameter("last_name", last_name) :
                new ObjectParameter("last_name", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var shoe_sizeParameter = shoe_size.HasValue ?
                new ObjectParameter("shoe_size", shoe_size) :
                new ObjectParameter("shoe_size", typeof(double));
    
            var weightParameter = weight.HasValue ?
                new ObjectParameter("weight", weight) :
                new ObjectParameter("weight", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateStudentInfo", student_idParameter, ssnParameter, first_nameParameter, last_nameParameter, emailParameter, passwordParameter, shoe_sizeParameter, weightParameter);
        }
    }
}
