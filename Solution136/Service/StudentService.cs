namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class StudentService
    {
        private readonly IStudentRepository repository;

        public StudentService(IStudentRepository repository)
        {
            this.repository = repository;
        }

        public void InsertStudent(student student, ref List<string> errors)
        {
            if (student == null)
            {
                errors.Add("Student cannot be null");
                throw new ArgumentException();
            }

            if (student.student_id.Length < 5)
            {
                errors.Add("Invalid student ID");
                throw new ArgumentException();
            }

            this.repository.InsertStudent(student, ref errors);
        }

        public void UpdateStudent(student student, ref List<string> errors)
        {
            if (student == null)
            {
                errors.Add("Student cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(student.student_id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (student.student_id.Length < 5)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            this.repository.UpdateStudent(student, ref errors);
        }

        public student GetStudent(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            return this.repository.GetStudentDetail(id, ref errors);
        }

        public void DeleteStudent(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            this.repository.DeleteStudent(id, ref errors);
        }

        public List<student> GetStudentList(ref List<string> errors)
        {
            return this.repository.GetStudentList(ref errors);
        }

        public void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || scheduleId < 0)
            {
                errors.Add("Invalid student id or schedule id");
                throw new ArgumentException();
            }

            this.repository.EnrollSchedule(studentId, scheduleId, ref errors);
        }

        public void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || scheduleId < 0)
            {
                errors.Add("Invalid student id or schedule id");
                throw new ArgumentException();
            }

            this.repository.DropEnrolledSchedule(studentId, scheduleId, ref errors);
        }

        public List<enrollment> GetEnrollments(string studentId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            return this.repository.GetEnrollments(studentId, ref errors);
        }

        public float CalculateGpa(string studentId, ref List<string> errors)
        {
            var enrollments = this.repository.GetEnrollments(studentId, ref errors);

            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (enrollments == null)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();                
            }

            if (enrollments.Count == 0)
            {
                return 0.0f;
            }

            var sum = 0.0f;

            foreach (var enrollment in enrollments)
            {
                sum += enrollment.GradeValue;
            }

            return sum / enrollments.Count;
        }

        public float GetGradeValueFromGrade(string grade)
        {
            switch (grade)
            {
                case "A+":
                    return 4.3f;          
                case "A":
                    return 4.0f;
                case "A-":
                    return 3.7f;
                case "B+":
                    return 3.3f;
                case "B":
                    return 3f;
                case "B-":
                    return 2.7f;
                case "C+":
                    return 2.3f;
                case "C":
                    return 2f;
                case "C-":
                    return 1.7f;
                case "D":
                    return 1f;
                case "F":
                    return 0f;
                default:
                    throw new ArgumentException();  
            }
        }
    }
}
