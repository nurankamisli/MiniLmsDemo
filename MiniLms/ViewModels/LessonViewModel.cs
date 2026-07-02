using System.ComponentModel.DataAnnotations;

namespace MiniLms.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ders başlığı zorunludur.")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Ders sırası zorunludur.")]
        [Range(1, 1000)]
        public int Order { get; set; }

        public int CourseId { get; set; }
    }
}