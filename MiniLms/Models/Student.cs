using System.ComponentModel.DataAnnotations;

namespace MiniLms.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Öğrenci numarası zorunludur.")]
        public string StudentNumber { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}