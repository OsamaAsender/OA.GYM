using AutoMapper;
using OA.GYM.Entities;
using OA.GYM.Web.Models.Coaches;

namespace OA.GYM.Web.AutoMapperProfiles
{
    public class CoachAutoMapperProfile : Profile
    {
        public CoachAutoMapperProfile()
        {
            CreateMap<Coach, CoachDetailViewModel>();
            CreateMap<Coach, CoachListViewModel>();
            CreateMap<Coach, CoachViewModel>().ReverseMap();
        }
    }
}
