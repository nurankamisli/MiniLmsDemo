using System.Collections.Generic;

namespace MiniLms.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int WeekNumber { get; set; }

        public Course Course { get; set; } = null!;

        public ICollection<LessonContent> Contents { get; set; } = new List<LessonContent>();
    }
}