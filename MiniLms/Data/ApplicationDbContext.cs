using Microsoft.EntityFrameworkCore;
using MiniLms.Models;

namespace MiniLms.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Veritabanı Tablo Setleri (DbSets)
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        // ÖNEMLİ: Burası 'DbSet<object>' ise kesinlikle 'DbSet<CourseDocument>' yapın!
        public DbSet<CourseDocument> CourseDocuments { get; set; }

        // Data/ApplicationDbContext.cs dosyasının içine eklenecek:
        public DbSet<LessonContent> LessonContents { get; set; }
        // Data/ApplicationDbContext.cs içerisine eklenecek:
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Enrollment Tablosu Birincil Anahtar Yapılandırması
            // Modelinizdeki otomatik artan 'Id' alanını anahtar olarak belirliyoruz.
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.Id);

            // 2. Çoka-Çok İlişki (Many-to-Many) ve Yabancı Anahtar Bağlantıları

            // Enrollment -> Student Bağlantısı:
            // Bir kaydın bir öğrencisi olur; bir öğrenci birden fazla ders kaydına sahip olabilir.
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // Öğrenci silinirse kayıtları da silinsin

            // Enrollment -> Course Bağlantısı:
            // Bir kaydın bir dersi olur; bir ders birden fazla ders kaydına sahip olabilir.
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            // Ders silinirse kayıtları da silinsin

            modelBuilder.Entity<CourseDocument>()
                .HasOne(hd => hd.Course)
                .WithMany(c => c.Documents)
                .HasForeignKey(hd => hd.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}