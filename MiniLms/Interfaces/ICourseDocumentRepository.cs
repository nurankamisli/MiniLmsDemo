using MiniLms.Models;

namespace MiniLms.Interfaces
{
    public interface ICourseDocumentRepository : IGenericRepository<CourseDocument>
    {
        // Temel CRUD işlemleri GenericRepository'den otomatik gelecektir.
    }
}