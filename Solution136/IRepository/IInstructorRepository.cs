namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IInstructorRepository
    {
        List<instructor> GetList(ref List<string> errors, string name = null, string title = null);

        void UpdateInstructor(instructor instructor, ref List<string> errors);

        void InsertInstructor(instructor instructor, ref List<string> errors);

        instructor GetDetail(int id, ref List<string> errors);
    }
}
