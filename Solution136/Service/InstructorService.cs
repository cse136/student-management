namespace Service
{
    using System.Collections.Generic;

    using IRepository;

    using POCO;

    public class InstructorService
    {
        private readonly IInstructorRepository repository;

        public InstructorService(IInstructorRepository repository)
        {
            this.repository = repository;
        }

        public List<instructor> GetList(ref List<string> errors, string name = null, string title = null)
        {
            return this.repository.GetList(ref errors, name, title);
        }

        public void Edit(instructor instructor, ref List<string> errors)
        {
            this.repository.UpdateInstructor(instructor, ref errors);
        }

        public void Add(instructor instructor, ref List<string> errors)
        {
            this.repository.InsertInstructor(instructor, ref errors);
        }

        public instructor GetDetail(int id, ref List<string> errors)
        {
            return this.repository.GetDetail(id, ref errors);
        }
    }
}
