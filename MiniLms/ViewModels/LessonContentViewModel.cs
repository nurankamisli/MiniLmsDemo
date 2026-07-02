using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MiniLms.Models.Enums;

namespace MiniLms.ViewModels;

public class LessonContentViewModel
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "İçerik")]
    public string? Body { get; set; }

    [Display(Name = "Video / PDF Linki")]
    public string? ResourceUrl { get; set; }

    [Display(Name = "PDF Dosyası")]
    public IFormFile? File { get; set; }

    [Required]
    public ContentType Type { get; set; }

    public int Order { get; set; }
}