using MiniLms.Interfaces;
using MiniLms.Models;

namespace MiniLms.Services
{
    public class LessonContentService : ILessonContentService
    {
        private readonly ILessonContentRepository _repository;

        public LessonContentService(ILessonContentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LessonContent>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<LessonContent>> GetByLessonIdAsync(int lessonId)
        {
            return await _repository.GetByLessonIdAsync(lessonId);
        }

        public async Task<LessonContent?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(LessonContent entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Order kontrolü (aynı lesson içinde çakışma olmasın)
            var existing = await _repository.GetByLessonAndOrderAsync(entity.LessonId, entity.Order);

            if (existing != null)
                throw new InvalidOperationException(
                    $"Bu ders içerisinde {entity.Order}. sırada içerik zaten var.");

            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(LessonContent entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingEntity = await _repository.GetByIdAsync(entity.Id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Güncellenecek içerik bulunamadı.");

            var conflict = await _repository.GetByLessonAndOrderAsync(entity.LessonId, entity.Order);

            if (conflict != null && conflict.Id != entity.Id)
                throw new InvalidOperationException(
                    $"Bu ders içerisinde {entity.Order}. sırada başka bir içerik var.");

            existingEntity.Title = entity.Title;
            existingEntity.Body = entity.Body;
            existingEntity.ResourceUrl = entity.ResourceUrl;
            existingEntity.Type = entity.Type;
            existingEntity.Order = entity.Order;

            await _repository.UpdateAsync(existingEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("Silinecek içerik bulunamadı.");

            await _repository.DeleteAsync(id);
        }
    }
}