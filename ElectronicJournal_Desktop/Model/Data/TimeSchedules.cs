using System.Collections.Generic;

namespace ElectronicJournal_Desktop.Model.Data
{
    public partial class TimeSchedules
    {
        public TimeSchedules()
        {
            Lessons = new HashSet<Lessons>();
        }

        public int TimeScheduleId { get; set; }
        public string TimeInterval { get; set; }

        public virtual ICollection<Lessons> Lessons { get; set; }
    }
}
