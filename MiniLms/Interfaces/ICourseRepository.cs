using MiniLms.Models;
namespace MiniLms.Interfaces
{
    public interface ICourseRepository:IGenericRepository<Course>
    {
        //ders koduna göre kursu getiren metot
        Task<Course?> GetByCourseCodeAsync(string courseCode);
    }
}
