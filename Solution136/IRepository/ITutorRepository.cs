namespace IRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using POCO;

    public interface ITutorRepository 
    {
        void InsertTutor(schedule_tutor student, ref List<string> errors);
       
        void UpdateTutor(schedule_tutor student, ref List<string> errors);
        
        void DeleteTutor(string student_id, int schedule_id, ref List<string> errors);
        
        List<schedule_tutor> GetStudentTutorings(string student_id, ref List<string> errors);

        // List<schedule_tutor> GetCourseTutorings(int course_id, string session, int year, ref List<string> errors);
    }
}
