using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.Models
{
    public class Room
    {
        public Guid  Id { get; set; }

        public string Name { get; set; }

        public ICollection<Lectures> Lectures { get; set; }
    }
}
