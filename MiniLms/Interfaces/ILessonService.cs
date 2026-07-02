using MiniLms.Models;

namespace MiniLms.Interfaces
{
    public interface ILessonService
    {
        Task<IEnumerable<Lesson>> GetAllAsync();

        Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId);

        Task<Lesson?> GetByIdAsync(int id);

        Task AddAsync(Lesson entity);

        Task UpdateAsync(Lesson entity);

        Task DeleteAsync(int id);
    }
}