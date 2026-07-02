using MiniLms.Models;

namespace MiniLms.Interfaces
{
    public interface ILessonContentRepository : IGenericRepository<LessonContent>
    {
        Task<IEnumerable<LessonContent>> GetByLessonIdAsync(int lessonId);

        Task<LessonContent?> GetByLessonAndOrderAsync(int lessonId, int order);
        Task MarkAsIndexedAsync(object ıd);
        Task<IEnumerable<LessonContent>> GetUnIndexedAsync();
    }
}