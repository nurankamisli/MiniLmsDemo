using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniLms.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Tüm kayıtları listelemek için kullanılır
        Task<IEnumerable<T>> GetAllAsync();

        // Benzersiz (ID) değerine göre tek bir kayıt getirmek için kullanılır
        Task<T?> GetByIdAsync(int id);

        // Veritabanına yeni bir kayıt eklemek için kullanılır
        Task AddAsync(T entity);

        // Mevcut bir kaydı güncellemek için kullanılır
        Task UpdateAsync(T entity);

        // Bir kaydı sistemden silmek için kullanılır
        Task DeleteAsync(int id);
    }
}