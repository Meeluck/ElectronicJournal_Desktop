﻿
namespace ElectronicJournal_Desktop.Model.Data
{
    public partial class StudentGroups
    {
        public int StudentGroupId { get; set; }
        public int UserId { get; set; }
        public int? GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Users User { get; set; }
    }
}
