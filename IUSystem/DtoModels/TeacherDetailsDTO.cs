using IUSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.DtoModels
{
    public class TeacherDetailsDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public virtual ICollection<LecturesDTO> Lectures { get; set; }
    }
}
