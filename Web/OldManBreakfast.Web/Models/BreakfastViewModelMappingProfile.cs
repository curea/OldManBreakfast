using AutoMapper;
using OldManBreakfast.Data.Models;

namespace OldManBreakfast.Web.Models
{
    public class BreakfastViewModelMappingProfile : Profile
    {
          public BreakfastViewModelMappingProfile()
        {
            CreateMap<Breakfast, BreakfastViewModel>()
                //.ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => (int)src.ProjectId))
                .ReverseMap();
        }
    }
}