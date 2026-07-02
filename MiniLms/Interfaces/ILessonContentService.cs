using MiniLms.Models;

namespace MiniLms.Interfaces
{
    public interface ILessonContentService
    {
        Task<IEnumerable<LessonContent>> GetAllAsync();

        Task<IEnumerable<LessonContent>> GetByLessonIdAsync(int lessonId);

        Task<LessonContent?> GetByIdAsync(int id);

        Task AddAsync(LessonContent entity);

        Task UpdateAsync(LessonContent entity);

        Task DeleteAsync(int id);
    }
}