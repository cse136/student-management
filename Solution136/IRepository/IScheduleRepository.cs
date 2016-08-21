namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IScheduleRepository
    {
        List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors);

        course_schedule GetScheduledCourseDetails(int schedule_id, ref List<string> errors);
    }
}
