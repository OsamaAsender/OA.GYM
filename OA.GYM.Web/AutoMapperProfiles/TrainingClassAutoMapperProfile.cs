using AutoMapper;
using OA.GYM.Entities;
using OA.GYM.Web.Models.TrainingClasses;

namespace OA.GYM.Web.AutoMapperProfiles
{
    public class TrainingClassAutoMapperProfile : Profile
    {
        public TrainingClassAutoMapperProfile()
        {
            CreateMap<TrainingClass, TrainingClassesViewModel>().ReverseMap();
            CreateMap<TrainingClass, TrainingClassDetailViewModel>();
            CreateMap<TrainingClass, TrainingClassListViewModel>();
        }
    }
}
