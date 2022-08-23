using AutoMapper;
using OA.GYM.Entities;
using OA.GYM.Web.Models.ClassTypes;

namespace OA.GYM.Web.AutoMapperProfiles
{
    public class ClassTypeAutoMapperProfile :Profile
    {
        public ClassTypeAutoMapperProfile()
        {
            CreateMap<ClassType, ClassTypesViewModel>().ReverseMap();
        }
    }
}
