﻿using System.Collections.Generic;

namespace ElectronicJournal_Desktop.Model.Data
{
    public partial class Classrooms
    {
        public Classrooms()
        {
            Lessons = new HashSet<Lessons>();
        }

        public int ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public int? BuildingId { get; set; }

        public virtual Buildings Building { get; set; }
        public virtual ICollection<Lessons> Lessons { get; set; }
    }
}
