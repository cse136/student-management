namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository;
    using POCO;

    public class TutorService
    {
        private static readonly double MINIMUMGPAFORTUTORING = 3.0;

        private readonly ITutorRepository repository;
        private readonly StudentService studentService;
        private readonly ScheduleService scheduleService;

        public TutorService(ITutorRepository repository, IStudentRepository studentRepository, IScheduleRepository scheduleRepository)
        {
            this.repository = repository;
            this.studentService = new StudentService(studentRepository);
            this.scheduleService = new ScheduleService(scheduleRepository);
        }

        public void InsertTutor(schedule_tutor tutor, ref List<string> errors)
        {
            if (tutor == null)
            {
                errors.Add("Tutor cannot be null");
                throw new ArgumentNullException();
            }
            
            var enrollments = this.studentService.GetEnrollments(tutor.student_id, ref errors).ToList();

            if (errors.Count > 0) 
            {
                throw new Exception();
            }

            var enrollmentsWithGrade = enrollments.Where(x => x.grade != null).ToList();

            var course = this.scheduleService.GetScheduledCourseDetails(tutor.schedule_id, ref errors);

            if (course == null)
            {
                errors.Add("The course doesn't exist");
                throw new Exception();
            }
            
            var classTaken = enrollmentsWithGrade.Where(e => e.course_schedule.course_id == course.course_id).FirstOrDefault();

            if (classTaken == null)
            {
                errors.Add("Student needs to take class before tutoring");
            } 
            else if (this.studentService.GetGradeValueFromGrade(classTaken.grade) < MINIMUMGPAFORTUTORING)
            {
                errors.Add("Student has not the minimum GPA for tutoring the class");
            }

            var studentTutorings = this.repository.GetStudentTutorings(tutor.student_id, ref errors);

            if (studentTutorings.Count(st =>

                   st.course_schedule.year == course.year
                && st.course_schedule.session == course.session
                && st.availability_day_id == course.schedule_day_id
                && st.availability_time_id == course.schedule_time_id) > 0
                
                ||

                enrollments.Count(e =>
                       e.course_schedule.year == course.year
                    && e.course_schedule.session == course.session
                    && e.course_schedule.schedule_day_id == course.schedule_day_id
                    && e.course_schedule.schedule_time_id == course.schedule_time_id) > 0)
            {
                errors.Add("There is a conflict on the student schedule");
            }

            if (studentTutorings.Count(st => st.schedule_id == course.schedule_id) > 0)
            {
                errors.Add("Student already tutoring scheduled course");
            }

            if (tutor.availability_day_id == course.schedule_day_id && tutor.availability_time_id == course.schedule_time_id)
            {
                errors.Add("Can't tutor at the same time as the class");
            }

            if (errors.Count() == 0)
            {
                this.repository.InsertTutor(tutor, ref errors);
            }
        }

        public void UpdateTutor(schedule_tutor tutor, ref List<string> errors)
        {
            if (tutor == null)
            {
                errors.Add("Tutor cannot be null");
                throw new ArgumentException();
            }

            if (tutor.student_id == null)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            var enrollments = this.studentService.GetEnrollments(tutor.student_id, ref errors).ToList();

            if (errors.Count > 0)
            {
                throw new Exception();
            }

            var enrollmentsWithGrade = enrollments.Where(x => x.grade != null).ToList();

            var course = this.scheduleService.GetScheduledCourseDetails(tutor.schedule_id, ref errors);

            if (course == null)
            {
                errors.Add("The course doesn't exist");
                throw new Exception();
            }

            var studentTutorings = this.repository.GetStudentTutorings(tutor.student_id, ref errors);

            if (studentTutorings.Count(st =>

                   st.course_schedule.year == course.year
                && st.course_schedule.session == course.session
                && st.availability_day_id == course.schedule_day_id
                && st.availability_time_id == course.schedule_time_id) > 0

                ||

                enrollments.Count(e =>
                       e.course_schedule.year == course.year
                    && e.course_schedule.session == course.session
                    && e.course_schedule.schedule_day_id == course.schedule_day_id
                    && e.course_schedule.schedule_time_id == course.schedule_time_id) > 0)
            {
                errors.Add("There is a conflict on the student schedule");
            }

            if (tutor.availability_day_id == course.schedule_day_id && tutor.availability_time_id == course.schedule_time_id)
            {
                errors.Add("Can't tutor at the same time as the class");
            }

            if (errors.Count() == 0)
            {
                this.repository.UpdateTutor(tutor, ref errors);
            }
        }

        public void DeleteTutor(string id, int schedule_id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Tutor id is invalid");
                throw new ArgumentException();
            }

            this.repository.DeleteTutor(id, schedule_id, ref errors);
        }

        public List<schedule_tutor> GetStudentTutorings(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Tutor id is invalid");
                throw new ArgumentException();
            }

            return this.repository.GetStudentTutorings(id, ref errors);
        }
    }
}
