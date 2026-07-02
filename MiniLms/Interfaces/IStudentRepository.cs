using MiniLms.Models;

namespace MiniLms.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student> // Not: Projedeki imla hatasına (IGeenericRepository) sadık kalınmıştır.
    {
        // Temel CRUD dışındaki öğrenciye özel metotlar
        Task<Student?> GetStudentWithEnrollmentsAsync(int id);
        Task<Student?> GetByStudentNumberAsync(string studentNumber);
        Task SaveChangesAsync();
    }
}