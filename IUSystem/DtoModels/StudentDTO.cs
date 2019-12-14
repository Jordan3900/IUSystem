using IUSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.DtoModels
{
    public class StudentDTO
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }
        public string Subject { get; set; }

        public double Grade { get; set; }
    }
}
