using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniLms.Data;
using MiniLms.Interfaces;
using MiniLms.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLms.Services
{
    public class CourseDocumentService : ICourseDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseDocumentService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task SaveDocumentAsync(int courseId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Geçersiz dosya!");

            // 1. Dosyaların yükleneceği fiziksel klasör yolunu belirle (wwwroot/uploads)
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            // Eğer klasör yoksa otomatik oluştur
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // 2. Sunucuda aynı isimde dosya çakışmasını önlemek için benzersiz bir isim üret (GUID)
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // 3. Dosyayı fiziksel olarak sunucu diskine kaydet
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // 4. Veri tabanına kaydetmek için nesneyi hazırla
            var document = new CourseDocument
            {
                CourseId = courseId,
                FileName = file.FileName, // Kullanıcının gördüğü isim (Örn: Ödev1.pdf)
                FilePath = "/uploads/" + uniqueFileName, // Siteden erişilecek web adresi
                UploadedDate = DateTime.Now
            };

            await _context.CourseDocuments.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseDocument>> GetDocumentsByCourseIdAsync(int courseId)
        {
            return await _context.CourseDocuments
                .Where(d => d.CourseId == courseId)
                .OrderByDescending(d => d.UploadedDate)
                .ToListAsync();
        }

        public async Task<CourseDocument?> GetDocumentByIdAsync(int id)
        {
            // DÜZELTİLEN KISIM: Başına 'await' eklenerek derleme hatası kesin olarak çözüldü
            return await _context.CourseDocuments.FindAsync(id);
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var document = await _context.CourseDocuments.FindAsync(id);
            if (document != null)
            {
                // 1. Fiziksel dosyayı diskten sil
                string physicalPath = Path.Combine(_webHostEnvironment.WebRootPath, document.FilePath.TrimStart('/'));
                if (File.Exists(physicalPath))
                {
                    File.Delete(physicalPath);
                }

                // 2. Veri tabanı kaydını sil
                _context.CourseDocuments.Remove(document);
                await _context.SaveChangesAsync();
            }
        }

        public Task UploadDocumentAsync(int courseId, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}