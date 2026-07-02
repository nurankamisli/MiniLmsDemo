using MiniLms.Data;
using MiniLms.Interfaces;
using MiniLms.Models;

namespace MiniLms.Repositories
{
    public class CourseDocumentRepository
        : GenericRepository<CourseDocument>, ICourseDocumentRepository
    {
        public CourseDocumentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}