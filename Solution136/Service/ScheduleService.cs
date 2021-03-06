﻿namespace Service
{
    using System.Collections.Generic;

    using IRepository;

    using POCO;

    public class ScheduleService
    {
        private readonly IScheduleRepository repository;

        public ScheduleService(IScheduleRepository repository)
        {
            this.repository = repository;
        }

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            return this.repository.GetScheduleList(year, quarter, ref errors);
        }

        public course_schedule GetScheduledCourseDetails(int schedule_id, ref List<string> errors)
        {
            return this.repository.GetScheduledCourseDetails(schedule_id, ref errors);
        }
    }
}
