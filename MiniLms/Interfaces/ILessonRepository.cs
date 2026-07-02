using MiniLms.Models;

namespace MiniLms.Interfaces
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task DeleteAsync(Lesson lesson);

        // Belirli bir kursa ait tüm dersleri getirir.
        Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId);
    }
}