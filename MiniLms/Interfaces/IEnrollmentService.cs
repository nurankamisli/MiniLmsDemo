using MiniLms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniLms.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
        Task<Enrollment?> GetEnrollmentByIdAsync(int id);
        Task EnrollStudentAsync(Enrollment enrollment); // Ders kayıt iş kuralını çalıştırır
        Task RemoveEnrollmentAsync(int id);
    }
}