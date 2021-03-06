﻿using System.Collections.Generic;

namespace ElectronicJournal_Desktop.Model.Data
{
    public partial class Subjects
    {
        public Subjects()
        {
            Lessons = new HashSet<Lessons>();
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<Lessons> Lessons { get; set; }
    }
}
