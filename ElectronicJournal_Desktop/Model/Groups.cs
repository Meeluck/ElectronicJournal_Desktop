using System;
using System.Collections.Generic;

namespace ElectronicJournal_Desktop.Model
{
    public partial class Groups
    {
        public Groups()
        {
            GroupLessons = new HashSet<GroupLessons>();
            StudentGroups = new HashSet<StudentGroups>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime YearFormationGroup { get; set; }
        public int? StarostaId { get; set; }

        public virtual Users Starosta { get; set; }
        public virtual ICollection<GroupLessons> GroupLessons { get; set; }
        public virtual ICollection<StudentGroups> StudentGroups { get; set; }
    }
}
