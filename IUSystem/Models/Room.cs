﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.Models
{
    public class Room
    {
        public Room()
        {
            this.Lectures = new HashSet<Lectures>();
        }

        public string  Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Lectures> Lectures { get; set; }
    }
}
