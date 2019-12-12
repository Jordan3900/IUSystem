using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.DtoModels
{
    public class TeacherDTO
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Number { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<string> Lectures { get; set; }
    }
}
