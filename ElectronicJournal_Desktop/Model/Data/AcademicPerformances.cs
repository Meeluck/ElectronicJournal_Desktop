﻿
namespace ElectronicJournal_Desktop.Model.Data
{
    public partial class AcademicPerformances
    {
        public int AcademicPerformanceId { get; set; }
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public string Mark { get; set; }

        public virtual Lessons Lesson { get; set; }
        public virtual Users User { get; set; }
    }
}
