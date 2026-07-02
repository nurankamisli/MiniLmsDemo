using Microsoft.EntityFrameworkCore;
using MiniLms.Data;
using MiniLms.Interfaces;
using MiniLms.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLms.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task DeleteAsync(Lesson lesson)
        {
            throw new NotImplementedException();
        }

        // Bir kursa/derse ait tüm haftalık konuları getiren metot
        public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Lessons
                .AsNoTracking() // Performans optimizasyonu için takip mekanizmasını kapatıyoruz
                .Where(l => l.CourseId
                == courseId)
                .OrderBy(l => l.WeekNumber) // Haftalık sıralı gelmesi için
                .ToListAsync();
        }
    }
}