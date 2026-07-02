using AutoMapper;
using MiniLms.Interfaces;
using MiniLms.Models;
using MiniLms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLms.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public LessonService(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        // 1. Tüm Haftalık Ders Konularını Listeler
        public async Task<IEnumerable<LessonViewModel>> GetAllAsync()
        {
            var lessons = await _lessonRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LessonViewModel>>(lessons);
        }

        // 2. ID'ye Göre Belirli Bir Ders Konusunu Getirir
        public async Task<LessonViewModel> GetByIdAsync(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null) return null;

            return _mapper.Map<LessonViewModel>(lesson);
        }

        // 3. Yeni Bir Ders Konusu Ekleme (Örn: 5. Hafta Konusu)
        public async Task AddAsync(LessonViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var lesson = _mapper.Map<Lesson>(model);
            await _lessonRepository.AddAsync(lesson);
        }

        // 4. Mevcut Ders Konusunu Güncelleme
        public async Task UpdateAsync(LessonViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var lesson = _mapper.Map<Lesson>(model);
            await _lessonRepository.UpdateAsync(lesson);
        }

        // 5. Ders Konusunu Sistemden Kalıcı Olarak Silme
        public async Task DeleteAsync(int id)
        {
            await _lessonRepository.DeleteAsync(id);
        }

        // 6. KRİTİK METOT: Belirli bir kursa/derse ait tüm haftalık konuları filtreleyip sıralı getirir
        public async Task<IEnumerable<LessonViewModel>> GetByCourseIdAsync(int courseId)
        {
            var allLessons = await _lessonRepository.GetAllAsync();

            // Sadece ilgili CourseId ile eşleşenleri filtrele ve haftalara (WeekNumber) göre sırala
            var filteredLessons = allLessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.WeekNumber);

            return _mapper.Map<IEnumerable<LessonViewModel>>(filteredLessons);
        }

        Task<IEnumerable<Lesson>> ILessonService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Lesson>> ILessonService.GetByCourseIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        Task<Lesson?> ILessonService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Lesson entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Lesson entity)
        {
            throw new NotImplementedException();
        }
    }
}