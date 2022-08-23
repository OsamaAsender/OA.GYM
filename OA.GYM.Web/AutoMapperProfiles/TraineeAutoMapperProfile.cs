using AutoMapper;
using OA.GYM.Entities;
using OA.GYM.Web.Models.Trainees;

namespace OA.GYM.Web.AutoMapperProfiles
{
    public class TraineeAutoMapperProfile : Profile
    {
        public TraineeAutoMapperProfile()
        {
            CreateMap<Trainee, TraineesViewModel>().ReverseMap();
        }
    }
}