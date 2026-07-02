using MiniLms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniLms.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course entity); // İlk aşamada doğrudan entity veya ViewModel ile bağlayabiliriz
        Task UpdateCourseAsync(Course entity);
        Task DeleteCourseAsync(int id);
    }
}
