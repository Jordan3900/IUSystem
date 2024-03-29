﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.Models
{
    public class Lecture
    {
        public string Id { get; set; }

        public virtual Subject Subject { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual Room Room { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
