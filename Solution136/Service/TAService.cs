namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository;
    using POCO;
    using Repository;

    public class TAService
    {
        private readonly ITARepository repository;

        public TAService(ITARepository repository)
        {
            this.repository = repository;
        }

        public void InsertTA(TeachingAssistant ta, ref List<string> errors)
        {
            if (ta == null)
            {
                errors.Add("TA cannot be null");
                throw new ArgumentException();
            }

            if (ta.ta_id < 0)
            {
                errors.Add("Invalid TA ID");
                throw new ArgumentException();
            }

            this.ValidateTAType(ta.ta_type_id, ref errors);

            if (this.TAExists(ta.ta_id, ref errors))
            {
                errors.Add("Cannot insert duplicate TAs in a course");
                throw new ArgumentException();
            }
            
            if (string.IsNullOrEmpty(ta.last))
            {
                errors.Add("TA last name cannot be null or empty");
                throw new ArgumentException();
            }

            if (errors.Count > 0)
            {
                throw new Exception();
            }

            this.repository.InsertTA(ta, ref errors);
        }

        public void UpdateTA(TeachingAssistant ta, ref List<string> errors)
        {
            if (ta == null)
            {
                errors.Add("TA cannot be null");
                throw new ArgumentException();
            }

            if (ta.ta_id < 0)
            {
                errors.Add("Invalid TA ID");
                throw new ArgumentException();
            }

            this.TAExists(ta.ta_id, ref errors);

            this.ValidateTAType(ta.ta_type_id, ref errors);

            if (string.IsNullOrEmpty(ta.last))
            {
                errors.Add("TA last name cannot be null or empty");
                throw new ArgumentException();
            }

            if (errors.Count > 0)
            {
                throw new Exception();
            }

            this.repository.UpdateTA(ta, ref errors);
        }

        public ICollection<TeachingAssistant> GetCourseTAs(int schedule_id, ref List<string> errors)
        {
            if (schedule_id < 0)
            {
                errors.Add("Invalid TA id");
                throw new ArgumentException();
            }

            if (errors.Count > 0)
            {
                throw new Exception();
            }

            return this.repository.GetCourseTAs(schedule_id, ref errors);
        }

        public void DeleteTA(int ta_id, ref List<string> errors)
        {
            if (ta_id < 0)
            {
                errors.Add("Invalid TA ID");
                throw new ArgumentException();
            }
            
            if (errors.Count > 0)
            {
                throw new Exception();
            }

            this.repository.DeleteTA(ta_id, ref errors);
        }

        private void ValidateTAType(int ta_type_id, ref List<string> errors)
        {
            var types = this.repository.GetTATypes(ref errors);

            if (types.Count(tat => tat.ta_type_id == ta_type_id) != 1)
            {
                errors.Add("Invalid TA type ID");
                throw new ArgumentException();
            }
        }

        private bool TAExists(int ta_id, ref List<string> errors)
        {
            var tas = this.repository.GetTAs(ref errors);

            if (tas.Count(ta => ta.ta_id == ta_id) == 0)
            {
                errors.Add("TA does not exist");
                throw new ArgumentException();
            }

            return true;
        }
    }
}
