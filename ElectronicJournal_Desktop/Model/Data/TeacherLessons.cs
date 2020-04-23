
namespace ElectronicJournal_Desktop.Model.Data
{
    public partial class TeacherLessons
    {
        public int TeacherLessonId { get; set; }
        public int TeacherId { get; set; }
        public int LessonId { get; set; }

        public virtual Lessons Lesson { get; set; }
        public virtual Teachers Teacher { get; set; }
    }
}
