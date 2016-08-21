namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IStudentRepository
    {
        void InsertStudent(student student, ref List<string> errors);

        void UpdateStudent(student student, ref List<string> errors);

        void DeleteStudent(string id, ref List<string> errors);

        student GetStudentDetail(string id, ref List<string> errors);

        List<student> GetStudentList(ref List<string> errors);

        void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors);

        void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors);

        List<enrollment> GetEnrollments(string studentId, ref List<string> errors);
    }
}
