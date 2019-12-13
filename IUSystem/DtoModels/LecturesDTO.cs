using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IUSystem.DtoModels
{
    public class LecturesDTO
    {
        public string Id { get; set; }

        public virtual string Subject { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public virtual string Room { get; set; }
    }
}
