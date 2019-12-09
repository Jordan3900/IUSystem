using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.Models
{
    public class Subject
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Grade { get; set; }

        public virtual Student Student { get; set; }
    }
}
