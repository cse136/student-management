namespace IRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using POCO;

    public interface ITARepository
    {
        void InsertTA(TeachingAssistant ta, ref List<string> errors);

        ICollection<TeachingAssistant> GetCourseTAs(int schedule_id, ref List<string> errors);

        void UpdateTA(TeachingAssistant ta, ref List<string> errors);

        void DeleteTA(int ta_id, ref List<string> errors);

        ICollection<TeachingAssistantType> GetTATypes(ref List<string> errors);

        ICollection<TeachingAssistant> GetTAs(ref List<string> errors);
    }
}