using System.ComponentModel.DataAnnotations;

namespace MiniLms.ViewModels
{
    public class StudentEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="isim girmek zorunludur.")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Öğrenci numarası zorunludur.")]
        public string StudentNumber { get; set; }
    }
}
