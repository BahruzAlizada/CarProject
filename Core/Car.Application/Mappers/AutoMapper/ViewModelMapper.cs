using AutoMapper;
using Car.Application.ViewModels;
using Car.Domain.Entities;

namespace Car.Application.Mappers.AutoMapper
{
    public class ViewModelMapper : Profile
    {
        public ViewModelMapper()
        {
            CreateMap<Category, MarkaVM>().ReverseMap();
            CreateMap<Category,ModelVM>().ReverseMap();
        }
    }
}
