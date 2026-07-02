using AutoMapper;
using MiniLms.Models;
using MiniLms.ViewModels;

namespace MiniLms.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentCreateViewModel, Student>().ReverseMap();

            
            CreateMap<StudentEditViewModel, Student>().ReverseMap();


            CreateMap<Lesson, LessonViewModel>().ReverseMap();

            CreateMap<LessonContent, LessonContentViewModel>().ReverseMap();

        }
    }
}